using GraphQL.EntityFramework;
using StaplePuck.Core.Stats;

namespace StaplePuck.API.Graphs
{
    public class PlayerScoreGraph : EfObjectGraphType<PlayerScore>
    {
        public PlayerScoreGraph(IEfGraphQLService graphQLService) : base(graphQLService)
        {
            Field(x => x.Id);
            Field(x => x.AdminOverride);
            AddNavigationField<PlayerStatsOnDateGraph, PlayerStatsOnDate>(
                name: "playerStatsOnDate",
                resolve: context => context.Source.PlayerStatsOnDate);
            Field(x => x.ScoringType);
            AddNavigationField<ScoringTypeGraph, ScoringType>(
                name: "scoringType",
                resolve: context => context.Source.ScoringType);
            Field(x => x.Total);
        }
    }
}
