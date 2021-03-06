﻿using GraphQL.EntityFramework;
using StaplePuck.Core.Fantasy;
using StaplePuck.Data;

namespace StaplePuck.API.Graphs
{
    public class FantasyTeamGraph : EfObjectGraphType<StaplePuckContext, FantasyTeam>
    {
        public FantasyTeamGraph(IEfGraphQLService<StaplePuckContext> graphQLService) : base(graphQLService)
        {
            Field(x => x.Id);
            Field(x => x.Name, nullable: true);
            Field(x => x.IsPaid);
            Field(x => x.IsValid);
            Field(x => x.LeagueId);
            AddNavigationListField(
                name: "fantasyTeamPlayers",
                resolve: context => context.Source.FantasyTeamPlayers);
            AddNavigationField(
                name: "league",
                resolve: context => context.Source.League);
            AddNavigationField(
                name: "GM",
                resolve: context => context.Source.GM);

            Field(x => x.Rank);
            Field(x => x.Score);
            Field(x => x.TodaysScore);
        }
    }
}
