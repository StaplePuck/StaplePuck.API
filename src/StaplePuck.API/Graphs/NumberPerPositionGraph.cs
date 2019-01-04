using GraphQL.EntityFramework;
using StaplePuck.Core.Fantasy;
using StaplePuck.Core.Stats;

namespace StaplePuck.API.Graphs
{
    public class NumberPerPositionGraph : EfObjectGraphType<NumberPerPosition>
    {
        public NumberPerPositionGraph(IEfGraphQLService graphQLService) : base(graphQLService)
        {
            Field(x => x.Id);
            Field(x => x.Count);
            AddNavigationField<LeagueGraph, League>(
                name: "leauge",
                resolve: context => context.Source.League);
            AddNavigationField<PositionTypeGraph, PositionType>(
                name: "position",
                resolve: context => context.Source.Position);
        }
    }
}
