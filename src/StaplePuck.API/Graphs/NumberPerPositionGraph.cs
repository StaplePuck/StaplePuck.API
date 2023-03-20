using GraphQL.EntityFramework;
using StaplePuck.Core.Fantasy;
using StaplePuck.Core.Stats;
using StaplePuck.Data;

namespace StaplePuck.API.Graphs
{
    public class NumberPerPositionGraph : EfObjectGraphType<StaplePuckContext, NumberPerPosition>
    {
        public NumberPerPositionGraph(IEfGraphQLService<StaplePuckContext> graphQLService) : base(graphQLService)
        {
            Field(x => x.Count);
            AddNavigationField(
                name: "leauge",
                resolve: context => context.Source.League);
            AddNavigationField(
                name: "positionType",
                resolve: context => context.Source.PositionType);
        }
    }
}
