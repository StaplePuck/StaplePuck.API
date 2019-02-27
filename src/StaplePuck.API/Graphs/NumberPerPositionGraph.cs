using GraphQL.EntityFramework;
using StaplePuck.Core.Fantasy;
using StaplePuck.Core.Stats;

namespace StaplePuck.API.Graphs
{
    public class NumberPerPositionGraph : EfObjectGraphType<NumberPerPosition>
    {
        public NumberPerPositionGraph(IEfGraphQLService graphQLService) : base(graphQLService)
        {
            Field(x => x.Count);
            AddNavigationField<LeagueGraph, League>(
                name: "leauge",
                resolve: context => context.Source.League);
            AddNavigationField<PositionTypeGraph, PositionType>(
                name: "positionType",
                resolve: context => context.Source.PositionType);
        }
    }
}
