using GraphQL.EntityFramework;
using StaplePuck.Core.Fantasy;
using StaplePuck.Core.Stats;

namespace StaplePuck.API.Graphs
{
    public class ScoringRulePointsGraph : EfObjectGraphType<ScoringRulePoints>
    {
        public ScoringRulePointsGraph(IEfGraphQLService graphQLService) : base(graphQLService)
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
