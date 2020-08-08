using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Authorization;
using GraphQL.Types;
using StaplePuck.Core.Auth;
using StaplePuck.Core.Data;
using StaplePuck.Core.Fantasy;
using StaplePuck.Core.Stats;
using StaplePuck.API.Auth;
using StaplePuck.API.Graphs;
using StaplePuck.API.Graphs.Input;
using StaplePuck.API.Constants;
using Microsoft.Extensions.Logging;

namespace StaplePuck.API.Models
{
    public class Mutation : ObjectGraphType
    {
        public Mutation(IFantasyRepository fantasyRepository, IStatsRepository statsRepository, IOptions<Auth0APISettings> options, IAuthorizationClient authorizationClient, ILogger<Mutation> logger)
        {
            Name = "Mutation";

            Field<ResultGraph>(
                "updateUser",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<UserInputType>> { Name = "user" }
                ),
                resolve: context =>
                {
                    var user = context.GetArgument<User>("user");
                    user.Name = user?.Name?.Trim();
                    user.Email = user?.Email?.Trim();
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

                    if (context.Errors.Count > 0)
                    {
                        return new ResultModel { Id = -1, Success = false, Message = string.Empty };
                    }
                    user.ExternalId = subject;

                    return fantasyRepository.Update(user);
                });

            Field<ResultGraph>(
                "createSeason",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<SeasonInputType>> { Name = "season" }
                ),
                resolve: context =>
                {
                    var season = context.GetArgument<Season>("season");
                    return statsRepository.Add(season);
                }).AuthorizeWith(AuthorizationPolicyName.WriteStats);

            Field<ResultGraph>(
                "createLeague",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<LeagueCreateInputType>> { Name = "league" }
                ),
                resolve: context =>
                {
                    var league = context.GetArgument<League>("league");
                    return fantasyRepository.Add(league);
                }).AuthorizeWith(AuthorizationPolicyName.Admin);

            Field<ResultGraph>(
                "updateLeague",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<LeagueUpdateInputType>> { Name = "league" }
                ),
                resolve: context =>
                {
                    var league = context.GetArgument<League>("league");

                    if (!authorizationClient.UserIsCommissioner(((GraphQLUserContext)context.UserContext).User, league.Id))
                    {
                        context.Errors.Add(new GraphQL.ExecutionError("User is not authorized"));
                        return new ResultModel { Id = -1, Success = false, Message = string.Empty };
                    }

                    return fantasyRepository.Update(league);
                });

            Field<ResultGraph>(
                "createFantasyTeam",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<FantasyTeamCreateInputType>> { Name = "fantasyTeam" }
                ),
                resolve: context =>
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

                    if (context.Errors.Count > 0)
                    {
                        return new ResultModel { Id = -1, Success = false, Message = string.Empty };
                    }

                    return fantasyRepository.Add(team, subject);
                }).RequiresAuthorization();
            Field<ResultGraph>(
                "updateFantasyTeam",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<FantasyTeamUpdateInputType>> { Name = "fantasyTeam" }
                ),
                resolve: context =>
                {
                    var team = context.GetArgument<FantasyTeam>("fantasyTeam");

                    //var subject = ((GraphQLUserContext)context.UserContext).User.GetUserId(options.Value);
                    //if (!fantasyRepository.UserIsGM(team.Id, subject).Result)
                    var user = ((GraphQLUserContext)context.UserContext).User;
                    if (!authorizationClient.UserIsGM(user, team.Id) &&
                        !authorizationClient.UserIsCommissioner(user, team.LeagueId))
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
                }).RequiresAuthorization();

            Field<ResultGraph>(
                "updateGameDateStats",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<GameDateInputType>> { Name = "gameDate" }
                ),
                resolve: context =>
                {
                    var gameDate = context.GetArgument<GameDate>("gameDate");
                    return statsRepository.Update(gameDate);
                });//.AuthorizeWith(AuthorizationPolicyName.WriteStats);

            Field<ResultGraph>(
                "updateTeamStates",
                arguments: new QueryArguments(
                    new QueryArgument<ListGraphType<TeamStateForSeasonInputType>> { Name = "teamStates" }
                ),
                resolve: context =>
                {
                    var gameDate = context.GetArgument<TeamStateForSeason[]>("teamStates");
                    return statsRepository.Update(gameDate);
                });//.AuthorizeWith(AuthorizationPolicyName.WriteStats);

            Field<ResultGraph>(
                "updateLeagueScores",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<LeagueScoreInputType>> { Name = "league" }
                ),
                resolve: context =>
                {
                    var league = context.GetArgument<League>("league");
                    return statsRepository.Update(league);
                });//.AuthorizeWith(AuthorizationPolicyName.WriteStats);

            Field<ResultGraph>(
                "overridePlayerScore",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<PlayerStatsOnDateUpdateInputType>> { Name = "playerStats" }
                ),
                resolve: context =>
                {
                    var playerStats = context.GetArgument<PlayerStatsOnDate>("playerStats");
                    return statsRepository.Update(playerStats);
                });//.AuthorizeWith(AuthorizationPolicyName.WriteStats);

            Field<ResultGraph>(
                "addNotificationToken",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<NotificationTokenInputType>> { Name = "notificationToken" }
                ),
                resolve: context =>
                {
                    var notificationToken = context.GetArgument<NotificationToken>("notificationToken");
                    var subject = ((GraphQLUserContext)context.UserContext).User.GetUserId(options.Value);
                    if (string.IsNullOrEmpty(subject))
                    {
                        context.Errors.Add(new GraphQL.ExecutionError("User is not authenticated"));
                    }

                    if (context.Errors.Count > 0)
                    {
                        return new ResultModel { Id = -1, Success = false, Message = string.Empty };
                    }

                    return fantasyRepository.Add(notificationToken, subject);
                });

            Field<ResultGraph>(
                "removeNotificationToken",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<NotificationTokenInputType>> { Name = "notificationToken" }
                ),
                resolve: context =>
                {
                    var notificationToken = context.GetArgument<NotificationToken>("notificationToken");

                    return fantasyRepository.Remove(notificationToken);
                });
        }
    }
}
