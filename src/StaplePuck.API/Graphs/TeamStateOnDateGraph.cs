using GraphQL.EntityFramework;
using StaplePuck.Core.Stats;

namespace StaplePuck.API.Graphs
{
    public class TeamStateOnDateGraph : EfObjectGraphType<TeamStateOnDate>
    {
        public TeamStateOnDateGraph(IEfGraphQLService graphQLService) : base(graphQLService)
        {
            Field(x => x.Id);
            AddNavigationField<GameDateGraph, GameDate>(
                name: "gameDate",
                resolve: context => context.Source.GameDate);
            Field(x => x.GameState);
            Field(x => x.Team);
            AddNavigationField<GameDateGraph, GameDate>(
                name: "gameDate",
                resolve: context => context.Source.GameDate);
        }
    }
}
