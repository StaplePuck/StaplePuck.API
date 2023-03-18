using GraphQL.Types;
using Microsoft.Extensions.Options;
using StaplePuck.Core.Auth;
using StaplePuck.Core.Data;
using StaplePuck.Core.Fantasy;
using StaplePuck.Core.Stats;

public class Mutation : ObjectGraphType
{
    public Mutation(IFantasyRepository fantasyRepository, IStatsRepository statsRepository, IOptions<Auth0APISettings> options, IAuthorizationClient authorizationClient, ILogger<Mutation> logger)
    {
        Name = "Mutation";

        Field<ResultGraph>("updateUser")
            .Argument<NonNullGraphType<UserInputType>>("user")
            .Resolve(context =>
                {
                    var user = context.GetArgument<User>("user");
                    user.Name = user.Name.Trim();
                    user.Email = user.Email.Trim();
                    var subject = ((GraphQLUserContext)context.UserContext).User.GetUserId(options.Value);
                    if (string.IsNullOrEmpty(subject))
                    {
                        context.Errors.Add(new GraphQL.ExecutionError("User is not authenticated"));
                        // or a machine to machine account, but who cares
                    }
                    else
                    {
                        if (user.Name != null && fantasyRepository.UsernameAlreadyExists(user.Name, subject).Result)
                        {
                            context.Errors.Add(new GraphQL.ExecutionError($"Username '{user.Name}' already exists."));
                        }
                        if (user.Email != null && fantasyRepository.EmailAlreadyExists(user.Email, subject).Result)
                        {
                            context.Errors.Add(new GraphQL.ExecutionError($"Email '{user.Email} already exists."));
                        }
                    }

                    if (string.IsNullOrEmpty(subject) || context.Errors.Count > 0)
                    {
                        return new ResultModel { Id = -1, Success = false, Message = string.Empty };
                    }
                    user.ExternalId = subject;

                    return fantasyRepository.Update(user);
                });

        Field<ResultGraph>("createSeason")
            .Argument<NonNullGraphType<SeasonInputType>>("season")
            .Resolve(context =>
            {
                var season = context.GetArgument<Season>("season");
                return statsRepository.Add(season);
            }).AuthorizeWithPolicy(AuthorizationPolicyName.WriteStats);

        Field<ResultGraph>("createLeague")
            .Argument<NonNullGraphType<LeagueCreateInputType>>("league")
            .Resolve(context =>
            {
                var league = context.GetArgument<League>("league");
                return fantasyRepository.Add(league);
            }).AuthorizeWithPolicy(AuthorizationPolicyName.Admin);

        Field<ResultGraph>("updateLeague")
            .Argument<NonNullGraphType<LeagueUpdateInputType>>("league")
            .Resolve(context =>
            {
                var league = context.GetArgument<League>("league");

                if (!authorizationClient.UserIsCommissioner(((GraphQLUserContext)context.UserContext).User, league.Id))
                {
                    context.Errors.Add(new GraphQL.ExecutionError("User is not authorized"));
                    return new ResultModel { Id = -1, Success = false, Message = string.Empty };
                }

                return fantasyRepository.Update(league);
            });

        Field<ResultGraph>("createFantasyTeam")
            .Argument<NonNullGraphType<FantasyTeamCreateInputType>>("fantasyTeam")
            .Resolve(context =>
            {
                var team = context.GetArgument<FantasyTeam>("fantasyTeam");
                var subject = ((GraphQLUserContext)context.UserContext).User.GetUserId(options.Value);
                if (string.IsNullOrEmpty(subject))
                {
                    context.Errors.Add(new GraphQL.ExecutionError("User is not authenticated"));
                    // or a machine to machine account, but who cares
                }
                else
                {
                    context.Errors.AddRange(fantasyRepository.ValidateNew(team, subject).Result.Select(x => new GraphQL.ExecutionError(x)));
                }

                if (string.IsNullOrEmpty(subject) || context.Errors.Count > 0)
                {
                    return new ResultModel { Id = -1, Success = false, Message = string.Empty };
                }

                return fantasyRepository.Add(team, subject);
            }).Authorize();

        Field<ResultGraph>("updateFantasyTeam")
            .Argument<NonNullGraphType<FantasyTeamUpdateType>>("fantasyTeam")
            .Resolve(context =>
            {
                var team = context.GetArgument<FantasyTeam>("fantasyTeam");

                var subject = ((GraphQLUserContext)context.UserContext).User.GetUserId(options.Value);
                if (string.IsNullOrEmpty(subject))
                {
                    context.Errors.Add(new GraphQL.ExecutionError("User is not authenticated"));
                    return new ResultModel { Id = -1, Success = false, Message = string.Empty };
                }
                //if (!fantasyRepository.UserIsGM(team.Id, subject).Result)
                var user = ((GraphQLUserContext)context.UserContext).User;
                if (!authorizationClient.UserIsGM(user, team.Id) &&
                    !authorizationClient.UserIsCommissioner(user, team.LeagueId) &&
                    !fantasyRepository.ValidateUserIsAssignedGM(team.Id, subject).Result)
                {
                    context.Errors.Add(new GraphQL.ExecutionError("User is not authorized"));
                    return new ResultModel { Id = -1, Success = false, Message = string.Empty };
                }

                var validations = fantasyRepository.Validate(team).Result;
                var isValid = true;
                if (validations.Count > 0)
                {
                    isValid = false;
                    context.Errors.AddRange(validations.Select(x => new GraphQL.ExecutionError(x)));
                }
                return fantasyRepository.Update(team, isValid);
            }).Authorize();

        Field<ResultGraph>("updateGameDateStats")
            .Argument<NonNullGraphType<GameDateInputType>>("gameDate")
            .Resolve(context =>
            {
                var gameDate = context.GetArgument<GameDate>("gameDate");
                return statsRepository.Update(gameDate);
            });//.AuthorizeWith(AuthorizationPolicyName.WriteStats);

        Field<ResultGraph>("updateTeamStates")
            .Argument<ListGraphType<TeamStateForSeasonInputType>>("teamStates")
            .Resolve(context =>
            {
                var gameDate = context.GetArgument<TeamStateForSeason[]>("teamStates");
                return statsRepository.Update(gameDate);
            });//.AuthorizeWith(AuthorizationPolicyName.WriteStats);

        Field<ResultGraph>("updateLeagueScores")
            .Argument<NonNullGraphType<LeagueScoreInputType>>("league")
            .Resolve(context =>
            {
                var league = context.GetArgument<League>("league");
                return statsRepository.Update(league);
            });//.AuthorizeWith(AuthorizationPolicyName.WriteStats);

        Field<ResultGraph>("overridePlayerScore")
            .Argument<NonNullGraphType<PlayerStatsOnDateUpdateInputType>>("playerStats")
            .Resolve(context => 
            { 
                var playerStats = context.GetArgument<PlayerStatsOnDate>("playerStats"); 
                return statsRepository.Update(playerStats); 
            });//.AuthorizeWith(AuthorizationPolicyName.WriteStats);

        Field<ResultGraph>("addNotificationToken")
            .Argument<NonNullGraphType<NotificationTokenInputType>>("notificationToken")
            .Resolve(context =>
            {
                var notificationToken = context.GetArgument<NotificationToken>("notificationToken");
                var subject = ((GraphQLUserContext)context.UserContext).User.GetUserId(options.Value);
                if (string.IsNullOrEmpty(subject))
                {
                    context.Errors.Add(new GraphQL.ExecutionError("User is not authenticated"));
                }

                if (subject == null || context.Errors.Count > 0)
                {
                    return new ResultModel { Id = -1, Success = false, Message = string.Empty };
                }

                return fantasyRepository.Add(notificationToken, subject);
            });

        Field<ResultGraph>("removeNotificationToken")
            .Argument<NonNullGraphType<NotificationTokenInputType>>("notificationToken")
            .Resolve(context =>
            {
                var notificationToken = context.GetArgument<NotificationToken>("notificationToken");

                return fantasyRepository.Remove(notificationToken);
            });
    }
}

