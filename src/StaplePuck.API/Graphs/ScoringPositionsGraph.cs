using GraphQL.EntityFramework;
using StaplePuck.Core.Stats;
using StaplePuck.Data;

namespace StaplePuck.API.Graphs
{
    public class ScoringPositionsGraph : EfObjectGraphType<StaplePuckContext, ScoringPositions>
    {
        public ScoringPositionsGraph(IEfGraphQLService<StaplePuckContext> graphQLService) : base(graphQLService)
        {
            AddNavigationField(
                name: "positionType",
                resolve: context => context.Source.PositionType);
            AddNavigationField(
                name: "scoringType",
                resolve: context => context.Source.ScoringType);
        }
    }
}
