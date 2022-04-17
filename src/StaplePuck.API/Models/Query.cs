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
using GraphQL;

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
                    var dataContext = context.DbContext;
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
                    var dataContext = context.DbContext;
                    return dataContext.FantasyTeams;
                });

            AddQueryField(
                name: "leagues",
                resolve: context =>
                {
                    var dataContext = context.DbContext;
                    return dataContext.Leagues;
                });

            AddQueryField(
                name: "players",
                resolve: context =>
                {
                    var dataContext = context.DbContext;
                    return dataContext.Players;
                });

            AddQueryField(
                name: "playerSeasons",
                resolve: context =>
                {
                    return context.DbContext.PlayerSeasons;
                });

            AddQueryField(
                name: "scoringTypes",
                resolve: context =>
                {
                    return context.DbContext.ScoringTypes;
                });

            AddQueryField(
                name: "seasons",
                resolve: context =>
                {
                    return context.DbContext.Seasons;
                });

            AddQueryField(
                name: "sports",
                resolve: context =>
                {
                    return context.DbContext.Sports;
                });

            AddQueryField(
                name: "teams",
                resolve: context =>
                {
                    return context.DbContext.Teams;
                });

            AddQueryField(
                name: "users",
                resolve: context =>
                {
                    return context.DbContext.Users;
                });

            AddQueryField(
                name: "playerCalculatedScores",
                resolve: context =>
                {
                    return context.DbContext.PlayerCalculatedScores;
                });

            Field<ListGraphType<ScoringTypeHeaderGraph>>(
                name: "scoringTypeHeaders",
                resolve: context =>
                {
                    var id = context.GetArgument<int>("id");
                    var dbContext = ResolveDbContext(context);
                    var league = dbContext.Leagues.Include(x => x.ScoringRules).ThenInclude(x => x.ScoringType).FirstOrDefault(x => x.Id == id);
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
                    var dbContext = ResolveDbContext(context);
                    var team = dbContext.FantasyTeams.Include(x => x.League).ThenInclude(x => x.ScoringRules).ThenInclude(x => x.ScoringType).FirstOrDefault(x => x.Id == id);
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
                    var dbContext = ResolveDbContext(context);
                    var league = dbContext.Leagues.Include(x => x.FantasyTeams).ThenInclude(x => x.GM).FirstOrDefault(x => x.Id == id);
                    if (league != null)
                    {
                        if (league.IsLocked)
                        {
                            foreach (var fteam in league.FantasyTeams.Where(x => !x.IsPaid))
                            {
                                fteam.Score = -1;
                                fteam.TodaysScore = -1;
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
                    var dbContext = ResolveDbContext(context);
                    var league = dbContext.Leagues.Include(x => x.FantasyTeams).ThenInclude(x => x.GM).FirstOrDefault(x => x.Id == id);
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

            Field<ListGraphType<PlayerCalculatedScoreGraph>>(
                name: "playerCalculatedScoresForTeam",
                resolve: context =>
                {
                    
                    var leagueId = context.GetArgument<int>("leagueId");
                    var teamId = context.GetArgument<int>("teamId");
                    var dbContext = ResolveDbContext(context);
                    var pcs = dbContext.PlayerCalculatedScores
                        .Include(x => x.PlayerSeason)
                        .ThenInclude(x => x.PositionType)

                        .Include(x => x.PlayerSeason)
                        .ThenInclude(x => x.Team)

                        .Include(x => x.PlayerSeason)
                        .ThenInclude(x => x.TeamStateForSeason)

                        .Include(x => x.Player)
                        
                        .Include(x => x.League)

                        .Include(x => x.Scoring)
                        .ThenInclude(x => x.ScoringType)
                        .Where(x => x.LeagueId == leagueId);
                    if (pcs == null)
                    {
                        return null;
                    }
                    if (teamId > 0)
                    {
                        return pcs.Where(x => x.PlayerSeason.TeamId == teamId);
                    }
                    return pcs;
                },
                arguments: new QueryArguments(
                new QueryArgument<IdGraphType>
                {
                    Name = "leagueId"
                },
                new QueryArgument<IdGraphType>
                { 
                    Name = "teamId"
                }));

            AddQueryField(
                name: "myFantasyTeams",
                resolve: context =>
                {
                    var leagueId = context.GetArgument<int>("leagueId");
                    var dbContext = ResolveDbContext(context);
                    var user = ((GraphQLUserContext)context.UserContext).User;
                    if (user.Claims.Count() == 0)
                    {
                        return dbContext.FantasyTeams.Where(x => false);
                    }
                    var subject = user.GetUserId(options.Value);

                    if (leagueId > 0)
                    {
                        return dbContext.FantasyTeams.Where(x => x.GM.ExternalId == subject && x.LeagueId == leagueId);
                    }
                    return dbContext.FantasyTeams.Where(x => x.GM.ExternalId == subject);
                },
                arguments: new QueryArguments(
                new QueryArgument<IdGraphType>
                {
                    Name = "leagueId"
                }));
        }
    }
}
