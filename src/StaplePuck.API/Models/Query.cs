using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Authorization;
using GraphQL.EntityFramework;
using GraphQL.Types;
using StaplePuck.API.Graphs;
using StaplePuck.Core.Fantasy;
using StaplePuck.Core.Stats;
using StaplePuck.Data;

namespace StaplePuck.API.Models
{
    public class Query : EfObjectGraphType
    {
        private StaplePuckContext _staplePuckContext;
        public Query(IEfGraphQLService efGraphQlService, StaplePuckContext staplePuckContext) : base(efGraphQlService)
        {
            _staplePuckContext = staplePuckContext;
            //AddQueryField<FantasyTeamGraph, FantasyTeam>(
            //    name: "fantasyTeams",
            //    resolve: context =>
            //    {
            //        var dataContext = (StaplePuckContext)context.UserContext;
            //        return dataContext.FantasyTeams;
            //    });

            AddQueryField<FantasyTeamGraph, FantasyTeam>(
                name: "fantasyTeams",
                resolve: context =>
                {
                    var dataContext = (StaplePuckContext)context.UserContext;
                    return dataContext.FantasyTeams;
                });

            AddQueryField<LeagueGraph, League>(
                name: "leagues",
                resolve: context =>
                {
                    var dataContext = (StaplePuckContext)context.UserContext;
                    return dataContext.Leagues;
                });

            AddQueryField<PlayerGraph, Player>(
                name: "players",
                resolve: context =>
                {
                    var dataContext = (StaplePuckContext)context.UserContext;
                    return dataContext.Players;
                });

            AddQueryField<PlayerSeasonGraph, PlayerSeason>(
                name: "playerSeasons",
                resolve: context =>
                {
                    var dataContext = (StaplePuckContext)context.UserContext;
                    return dataContext.PlayerSeasons;
                });

            AddQueryField<ScoringTypeGraph, ScoringType>(
                name: "scoringTypes",
                resolve: context =>
                {
                    var dataContext = ((GraphQLUserContext)context.UserContext).DataContext;
                    return dataContext.ScoringTypes;
                });

            AddQueryField<SeasonGraph, Season>(
                name: "seasons",
                resolve: context =>
                {
                    var dataContext = (StaplePuckContext)context.UserContext;
                    return dataContext.Seasons;
                });

            AddQueryField<SportGraph, Sport>(
                name: "sports",
                resolve: context =>
                {
                    var dataContext = (StaplePuckContext)context.UserContext;
                    return dataContext.Sports;
                });

            AddQueryField<TeamGraph, Team>(
                name: "teams",
                resolve: context =>
                {
                    var dataContext = (StaplePuckContext)context.UserContext;
                    return dataContext.Teams;
                });

            AddQueryField<UserGraph, User>(
                name: "users",
                resolve: context =>
                {
                    var dataContext = (StaplePuckContext)context.UserContext;
                    return dataContext.Users;
                });
        }
    }
}
