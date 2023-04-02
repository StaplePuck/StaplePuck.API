using GraphQL.MicrosoftDI;
using GraphQL.Types;
using Microsoft.Extensions.Options;
using StaplePuck.Core.Auth;
using StaplePuck.Data.Repositories;
using StaplePuck.Core.Fantasy;
using StaplePuck.Core.Models;
using StaplePuck.Core.Stats;
using StaplePuck.Data;

public class Mutation : ObjectGraphType
{
    public Mutation(IFantasyRepository fantasyRepository, IStatsRepository statsRepository, IOptions<Auth0APISettings> options, IAuthorizationClient authorizationClient, ILogger<Mutation> logger)
    {
        Name = "Mutation";

        Field<ResultGraph>("updateUser")
            .Argument<NonNullGraphType<UserInputType>>("user")
            .Resolve()
            .WithScope()
            .WithService<StaplePuckContext>()
            .ResolveAsync(async (context, db) =>
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
                        if (!string.IsNullOrEmpty(user.Name) && fantasyRepository.UsernameAlreadyExists(db, user.Name, subject).Result)
                        {
                            context.Errors.Add(new GraphQL.ExecutionError($"Username '{user.Name}' already exists."));
                        }
                        if (!string.IsNullOrEmpty(user.Email) && fantasyRepository.EmailAlreadyExists(db,user.Email, subject).Result)
                        {
                            context.Errors.Add(new GraphQL.ExecutionError($"Email '{user.Email} already exists."));
                        }
                    }

                    if (string.IsNullOrEmpty(subject) || context.Errors.Count > 0)
                    {
                        return new ResultModel { Id = -1, Success = false, Message = string.Empty };
                    }
                    user.ExternalId = subject;

                    return await fantasyRepository.Update(db, user);
                });

        Field<ResultGraph>("createSeason")
            .Argument<NonNullGraphType<SeasonInputType>>("season")
            .Resolve()
            .WithScope()
            .WithService<StaplePuckContext>()
            .ResolveAsync(async (context, db) =>
            {
                var season = context.GetArgument<Season>("season");
                return await statsRepository.Add(db, season);
            }).AuthorizeWithPolicy(AuthorizationPolicyName.WriteStats);

        Field<ResultGraph>("createLeague")
            .Argument<NonNullGraphType<LeagueCreateInputType>>("league")
            .Resolve()
            .WithScope()
            .WithService<StaplePuckContext>()
            .ResolveAsync(async (context, db) =>
            {
                var league = context.GetArgument<League>("league");
                return await fantasyRepository.Add(db, league);
            }).AuthorizeWithPolicy(AuthorizationPolicyName.Admin);

        Field<ResultGraph>("updateLeague")
            .Argument<NonNullGraphType<LeagueUpdateInputType>>("league")
            .Resolve()
            .WithScope()
            .WithService<StaplePuckContext>()
            .ResolveAsync(async (context, db) =>
            {
                var league = context.GetArgument<League>("league");

                if (!authorizationClient.UserIsCommissioner(((GraphQLUserContext)context.UserContext).User, league.Id))
                {
                    context.Errors.Add(new GraphQL.ExecutionError("User is not authorized"));
                    return new ResultModel { Id = -1, Success = false, Message = string.Empty };
                }

                return await fantasyRepository.Update(db, league);
            });

        Field<ResultGraph>("createFantasyTeam")
            .Argument<NonNullGraphType<FantasyTeamCreateInputType>>("fantasyTeam")
            .Resolve()
            .WithScope()
            .WithService<StaplePuckContext>()
            .ResolveAsync(async (context, db) =>
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
                    context.Errors.AddRange(fantasyRepository.ValidateNew(db, team, subject).Result.Select(x => new GraphQL.ExecutionError(x)));
                }

                if (string.IsNullOrEmpty(subject) || context.Errors.Count > 0)
                {
                    return new ResultModel { Id = -1, Success = false, Message = string.Empty };
                }

                return await fantasyRepository.Add(db, team, subject);
            }).Authorize();

        Field<ResultGraph>("updateFantasyTeam")
            .Argument<NonNullGraphType<FantasyTeamUpdateType>>("fantasyTeam")
            .Resolve()
            .WithScope()
            .WithService<StaplePuckContext>()
            .ResolveAsync(async (context, db) =>
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
                    !fantasyRepository.ValidateUserIsAssignedGM(db, team.Id, subject).Result)
                {
                    context.Errors.Add(new GraphQL.ExecutionError("User is not authorized"));
                    return new ResultModel { Id = -1, Success = false, Message = string.Empty };
                }

                var validations = fantasyRepository.Validate(db, team).Result;
                var isValid = true;
                if (validations.Count > 0)
                {
                    isValid = false;
                    context.Errors.AddRange(validations.Select(x => new GraphQL.ExecutionError(x)));
                }
                return await fantasyRepository.Update(db, team, isValid);
            }).Authorize();

        Field<ResultGraph>("updateGameDateStats")
            .Argument<NonNullGraphType<GameDateInputType>>("gameDate")
            .Resolve()
            .WithScope()
            .WithService<StaplePuckContext>()
            .ResolveAsync(async (context, db) =>
            {
                var gameDate = context.GetArgument<GameDate>("gameDate");
                return await statsRepository.Update(db, gameDate);
            });//.AuthorizeWith(AuthorizationPolicyName.WriteStats);

        Field<ResultGraph>("updateTeamStates")
            .Argument<ListGraphType<TeamStateForSeasonInputType>>("teamStates")
            .Resolve()
            .WithScope()
            .WithService<StaplePuckContext>()
            .ResolveAsync(async (context, db) =>
            {
                var gameDate = context.GetArgument<TeamStateForSeason[]>("teamStates");
                return await statsRepository.Update(db, gameDate);
            });//.AuthorizeWith(AuthorizationPolicyName.WriteStats);

        Field<ResultGraph>("updateLeagueScores")
            .Argument<NonNullGraphType<LeagueScoreInputType>>("league")
            .Resolve()
            .WithScope()
            .WithService<StaplePuckContext>()
            .ResolveAsync(async (context, db) =>
            {
                var league = context.GetArgument<League>("league");
                return await statsRepository.Update(db, league);
            });//.AuthorizeWith(AuthorizationPolicyName.WriteStats);

        Field<ResultGraph>("overridePlayerScore")
            .Argument<NonNullGraphType<PlayerStatsOnDateUpdateInputType>>("playerStats")
            .Resolve()
            .WithScope()
            .WithService<StaplePuckContext>()
            .ResolveAsync(async (context, db) =>
            {
                var playerStats = context.GetArgument<PlayerStatsOnDate>("playerStats");
                return await statsRepository.Update(db, playerStats);
            });//.AuthorizeWith(AuthorizationPolicyName.WriteStats);

        Field<ResultGraph>("addNotificationToken")
            .Argument<NonNullGraphType<NotificationTokenInputType>>("notificationToken")
            .Resolve()
            .WithScope()
            .WithService<StaplePuckContext>()
            .ResolveAsync(async (context, db) =>
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

                return await fantasyRepository.Add(db, notificationToken, subject);
            });

        Field<ResultGraph>("removeNotificationToken")
            .Argument<NonNullGraphType<NotificationTokenInputType>>("notificationToken")
            .Resolve()
            .WithScope()
            .WithService<StaplePuckContext>()
            .ResolveAsync(async (context, db) =>
            {
                var notificationToken = context.GetArgument<NotificationToken>("notificationToken");

                return await fantasyRepository.Remove(db, notificationToken);
            });
    }
}

