using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using GraphQL.Authorization;
using GraphQL.EntityFramework;
using GraphQL.Types;
using StaplePuck.API.Auth;
using StaplePuck.API.Graphs;
using StaplePuck.Core;
using StaplePuck.Core.Auth;
using StaplePuck.Core.Fantasy;
using StaplePuck.Core.Stats;
using StaplePuck.Data;
using Microsoft.EntityFrameworkCore;


namespace StaplePuck.API.Models
{
    public class Query : QueryGraphType<StaplePuckContext>
    {
        public Query(IEfGraphQLService<StaplePuckContext> efGraphQlService, IOptions<Auth0APISettings> options) : base(efGraphQlService)
        {
            //AddQueryField<FantasyTeamGraph, FantasyTeam>(
            //    name: "fantasyTeams",
            //    resolve: context =>
            //    {
            //        var dataContext = (StaplePuckContext)context.UserContext;
            //        return dataContext.FantasyTeams;
            //    });
            
            AddSingleField(
                name: "currentUser",
                resolve: context =>
                {
                    var dataContext = ((GraphQLUserContext)context.UserContext).DataContext;
                    var user = ((GraphQLUserContext)context.UserContext).User;
                    if (user.Claims.Count() == 0)
                    {
                        throw new Exception("Not authenticated");
                    }
                    var subject = user.GetUserId(options.Value);
                    return dataContext.Users.Where(x => x.ExternalId == subject);
                }).RequiresAuthorization();

            AddQueryField(
                name: "fantasyTeams",
                resolve: context =>
                {
                    var dataContext = ((GraphQLUserContext)context.UserContext).DataContext;
                    return dataContext.FantasyTeams;
                });

            AddQueryField(
                name: "leagues",
                resolve: context =>
                {
                    var dataContext = ((GraphQLUserContext)context.UserContext).DataContext;
                    return dataContext.Leagues;
                });

            AddQueryField(
                name: "players",
                resolve: context =>
                {
                    var dataContext = ((GraphQLUserContext)context.UserContext).DataContext;
                    return dataContext.Players;
                });

            AddQueryField(
                name: "playerSeasons",
                resolve: context =>
                {
                    var dataContext = ((GraphQLUserContext)context.UserContext).DataContext;
                    return dataContext.PlayerSeasons;
                });

            AddQueryField(
                name: "scoringTypes",
                resolve: context =>
                {
                    var dataContext = ((GraphQLUserContext)context.UserContext).DataContext;
                    return dataContext.ScoringTypes;
                });

            AddQueryField(
                name: "seasons",
                resolve: context =>
                {
                    var dataContext = ((GraphQLUserContext)context.UserContext).DataContext;
                    return dataContext.Seasons;
                });

            AddQueryField(
                name: "sports",
                resolve: context =>
                {
                    var dataContext = ((GraphQLUserContext)context.UserContext).DataContext;
                    return dataContext.Sports;
                });

            AddQueryField(
                name: "teams",
                resolve: context =>
                {
                    var dataContext = ((GraphQLUserContext)context.UserContext).DataContext;
                    return dataContext.Teams;
                });

            AddQueryField(
                name: "users",
                resolve: context =>
                {
                    var dataContext = ((GraphQLUserContext)context.UserContext).DataContext;
                    return dataContext.Users;
                });

            Field<ListGraphType<ScoringTypeHeaderGraph>>(
                name: "scoringTypeHeaders",
                resolve: context =>
                {
                    var id = context.GetArgument<int>("id");
                    var dataContext = ((GraphQLUserContext)context.UserContext).DataContext;
                    var league = dataContext.Leagues.Include(x => x.ScoringRules).ThenInclude(x => x.ScoringType).FirstOrDefault(x => x.Id == id);
                    var scoring = league.ScoringRules.Where(x => x.PointsPerScore != 0).Select(x => x.ScoringType);
                    return scoring.Distinct();
                },
                arguments: new QueryArguments(
                new QueryArgument<IntGraphType>
                {
                    Name = "id"
                }));

            Field<ListGraphType<ScoringTypeHeaderGraph>>(
                name: "scoringTypeHeadersForTeam",
                resolve: context =>
                {
                    var id = context.GetArgument<int>("id");
                    var dataContext = ((GraphQLUserContext)context.UserContext).DataContext;
                    var team = dataContext.FantasyTeams.Include(x => x.League).ThenInclude(x => x.ScoringRules).ThenInclude(x => x.ScoringType).FirstOrDefault(x => x.Id == id);
                    var scoring = team.League.ScoringRules.Where(x => x.PointsPerScore != 0).Select(x => x.ScoringType);
                    return scoring.Distinct().OrderBy(x => x.Id);
                },
                arguments: new QueryArguments(
                new QueryArgument<IntGraphType>
                {
                    Name = "id"
                }));

            Field<LeagueGraph>(
                name: "leagueScores",
                resolve: context =>
                {
                    var id = context.GetArgument<int>("id");
                    var dataContext = ((GraphQLUserContext)context.UserContext).DataContext;
                    var league = dataContext.Leagues.Include(x => x.FantasyTeams).ThenInclude(x => x.GM).FirstOrDefault(x => x.Id == id);
                    if (league != null)
                    {
                        if (league.IsLocked)
                        {
                            foreach (var fteam in league.FantasyTeams.Where(x => !x.IsPaid))
                            {
                                fteam.Name = null;
                                fteam.GM = null;
                                fteam.Score = -1;
                                fteam.TodaysScore = -1;
                                fteam.Id = -1;
                            }
                        }
                    }
                    
                    return league;
                },
                arguments: new QueryArguments(
                new QueryArgument<IntGraphType>
                {
                    Name = "id"
                }));

            Field<ListGraphType<FantasyTeamGraph>>(
                name: "fantasyTeamsNotPaid",
                resolve: context =>
                {
                    var id = context.GetArgument<int>("id");
                    var dataContext = ((GraphQLUserContext)context.UserContext).DataContext;
                    var league = dataContext.Leagues.Include(x => x.FantasyTeams).ThenInclude(x => x.GM).FirstOrDefault(x => x.Id == id);
                    if (league == null || !league.IsLocked)
                    {
                        return null;
                    }
                    foreach (var fteam in league.FantasyTeams.Where(x => !x.IsPaid))
                    {
                        fteam.Score = -1;
                        fteam.TodaysScore = -1;
                    }
                    return league.FantasyTeams.Where(x => !x.IsPaid);
                },
                arguments: new QueryArguments(
                new QueryArgument<IntGraphType>
                {
                    Name = "id"
                }));
        }
    }
}
