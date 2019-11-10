using GraphQL.EntityFramework;
using StaplePuck.Core.Stats;
using StaplePuck.Data;

namespace StaplePuck.API.Graphs
{
    public class ScoringTypeGraph : EfObjectGraphType<StaplePuckContext, ScoringType>
    {
        public ScoringTypeGraph(IEfGraphQLService<StaplePuckContext> graphQLService) : base(graphQLService)
        {
            Field(x => x.Id);
            Field(x => x.Name);
            AddNavigationListField(
                name: "scoringPositions",
                resolve: context => context.Source.ScoringPositions);
            Field(x => x.ShortName);
            AddNavigationField(
                name: "sport",
                resolve: context => context.Source.Sport);
        }
    }
}
