using GraphQL.EntityFramework;
using StaplePuck.Core.Scoring;
using StaplePuck.Core.Stats;

namespace StaplePuck.API.Graphs
{
    public class CalculatedScoreItemGraph : EfObjectGraphType<CalculatedScoreItem>
    {
        public CalculatedScoreItemGraph(IEfGraphQLService graphQLService) : base(graphQLService)
        {
            Field(x => x.Score);
            AddNavigationField<ScoringTypeGraph, ScoringType>(
                name: "scoringType",
                resolve: context => context.Source.ScoringType);
            Field(x => x.TodaysScore);
            Field(x => x.TodaysTotal);
            Field(x => x.Total);
        }
    }
}
