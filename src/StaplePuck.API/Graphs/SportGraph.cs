using GraphQL.EntityFramework;
using StaplePuck.Core.Stats;
using StaplePuck.Data;

namespace StaplePuck.API.Graphs
{
    public class SportGraph : EfObjectGraphType<StaplePuckContext, Sport>
    {
        public SportGraph(IEfGraphQLService<StaplePuckContext> graphQLService) : base(graphQLService)
        {
            Field(x => x.Id);
            Field(x => x.Name);
            AddNavigationListField(
                name: "scoringTypes",
                resolve: context => context.Source.ScoringTypes);
        }
    }
}
