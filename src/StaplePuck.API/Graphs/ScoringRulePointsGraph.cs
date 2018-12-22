using GraphQL.EntityFramework;
using StaplePuck.Core.Fantasy;
using StaplePuck.Core.Stats;

namespace StaplePuck.API.Graphs
{
    public class ScoringRulePointsGraph : EfObjectGraphType<ScoringRulePoints>
    {
        public ScoringRulePointsGraph(IEfGraphQLService graphQLService) : base(graphQLService)
        {
            Field(x => x.Id);
            AddNavigationField<LeagueGraph, League>(
                name: "league",
                resolve: context => context.Source.League);
            Field(x => x.PointsPerScore);
            AddNavigationField<PositionTypeGraph, PositionType>(
                name: "position",
                resolve: context => context.Source.Position);
            AddNavigationField<ScoringTypeGraph, ScoringType>(
                name: "scoringType",
                resolve: context => context.Source.ScoringType);
        }
    }
}
