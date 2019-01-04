﻿using GraphQL.EntityFramework;
using StaplePuck.Core.Scoring;
using StaplePuck.Core.Stats;

namespace StaplePuck.API.Graphs
{
    public class PlayerCalculatedScoreGraph : EfObjectGraphType<PlayerCalculatedScore>
    {
        public PlayerCalculatedScoreGraph(IEfGraphQLService graphQLService) : base(graphQLService)
        {
            Field(x => x.Count);
            Field(x => x.GameState);
            AddNavigationField<PlayerGraph, Player>(
                name: "player",
                resolve: context => context.Source.Player);
            Field(x => x.Score);
            AddNavigationField<CalculatedScoreItemGraph, CalculatedScoreItem>(
                name: "scoring",
                resolve: context => context.Source.Scoring);
            AddNavigationField<TeamGraph, Team>(
                name: "team",
                resolve: context => context.Source.Team);
            Field(x => x.TodaysScore);
        }
    }
}