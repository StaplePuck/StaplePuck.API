using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Authorization;
using GraphQL.Types;
using StaplePuck.Core.Data;
using StaplePuck.Core.Fantasy;
using StaplePuck.Core.Stats;
using StaplePuck.API.Auth;
using StaplePuck.API.Graphs;
using StaplePuck.API.Graphs.Input;
using StaplePuck.API.Constants;

namespace StaplePuck.API.Models
{
    public class Mutation : ObjectGraphType
    {
        public Mutation(IFantasyRepository fantasyRepository, IStatsRepository statsRepository, IOptions<Auth.Auth0Settings> options)
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
                        if (fantasyRepository.UsernameAlreadyExists(user.Name, subject).Result)
                        {
                            context.Errors.Add(new GraphQL.ExecutionError($"Username '{user.Name}' already exists."));
                        }
                        if (fantasyRepository.EmailAlreadyExists(user.Email, subject).Result)
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
                "createFantasyTeam",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<FantasyTeamCreateInputType>> { Name = "fantasyTeam" }
                ),
                resolve: context =>
                {
                    var team = context.GetArgument<FantasyTeam>("fantasyTeam");
                    var subject = ((GraphQLUserContext)context.UserContext).User.GetUserId(options.Value);

                    return fantasyRepository.Add(team, subject);
                });
        }
    }
}
