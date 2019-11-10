using GraphQL.EntityFramework;
using StaplePuck.Core.Fantasy;
using StaplePuck.Core.Stats;
using StaplePuck.Data;

namespace StaplePuck.API.Graphs
{
    public class ScoringRulePointsGraph : EfObjectGraphType<StaplePuckContext, ScoringRulePoints>
    {
        public ScoringRulePointsGraph(IEfGraphQLService<StaplePuckContext> graphQLService) : base(graphQLService)
        {
            AddNavigationField(
                name: "league",
                resolve: context => context.Source.League);
            Field(x => x.PointsPerScore);
            Field(x => x.PositionTypeId);
            Field(x => x.ScoringTypeId);
            AddNavigationField(
                name: "positionType",
                resolve: context => context.Source.PositionType);
            AddNavigationField(
                name: "scoringType",
                resolve: context => context.Source.ScoringType);
        }
    }
}
