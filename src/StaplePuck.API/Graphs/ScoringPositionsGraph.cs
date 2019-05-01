using GraphQL.EntityFramework;
using StaplePuck.Core.Stats;

namespace StaplePuck.API.Graphs
{
    public class ScoringPositionsGraph : EfObjectGraphType<ScoringPositions>
    {
        public ScoringPositionsGraph(IEfGraphQLService graphQLService) : base(graphQLService)
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
