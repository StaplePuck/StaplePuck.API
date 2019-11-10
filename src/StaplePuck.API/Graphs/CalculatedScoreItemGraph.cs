using GraphQL.EntityFramework;
using StaplePuck.Core.Scoring;
using StaplePuck.Core.Stats;
using StaplePuck.Data;

namespace StaplePuck.API.Graphs
{
    public class CalculatedScoreItemGraph : EfObjectGraphType<StaplePuckContext, CalculatedScoreItem>
    {
        public CalculatedScoreItemGraph(IEfGraphQLService<StaplePuckContext> graphQLService) : base(graphQLService)
        {
            Field(x => x.Score);
            AddNavigationField(
                name: "scoringType",
                resolve: context => context.Source.ScoringType);
            Field(x => x.TodaysScore);
            Field(x => x.TodaysTotal);
            Field(x => x.Total);
        }
    }
}
