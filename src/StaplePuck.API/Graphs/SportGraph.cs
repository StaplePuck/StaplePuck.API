using GraphQL.EntityFramework;
using StaplePuck.Core.Stats;

namespace StaplePuck.API.Graphs
{
    public class SportGraph : EfObjectGraphType<Sport>
    {
        public SportGraph(IEfGraphQLService graphQLService) : base(graphQLService)
        {
            Field(x => x.Id);
            Field(x => x.Name);
            AddNavigationListField(
                name: "scoringTypes",
                resolve: context => context.Source.ScoringTypes);
        }
    }
}
