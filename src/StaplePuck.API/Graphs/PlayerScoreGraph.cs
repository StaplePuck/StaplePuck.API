using GraphQL.EntityFramework;
using StaplePuck.Core.Stats;
using StaplePuck.Data;

namespace StaplePuck.API.Graphs
{
    public class PlayerScoreGraph : EfObjectGraphType<StaplePuckContext, PlayerScore>
    {
        public PlayerScoreGraph(IEfGraphQLService<StaplePuckContext> graphQLService) : base(graphQLService)
        {
            Field(x => x.Id);
            Field(x => x.AdminOverride);
            Field(x => x.ScoringTypeId);
            AddNavigationField(
                name: "playerStatsOnDate",
                resolve: context => context.Source.PlayerStatsOnDate);
            AddNavigationField(
                name: "scoringType",
                resolve: context => context.Source.ScoringType);
            Field(x => x.Total);
        }
    }
}
