using GraphQL.EntityFramework;
using StaplePuck.Core.Stats;

namespace StaplePuck.API.Graphs
{
    public class TeamStateOnDateGraph : EfObjectGraphType<TeamStateOnDate>
    {
        public TeamStateOnDateGraph(IEfGraphQLService graphQLService) : base(graphQLService)
        {
            Field(x => x.Id);
            Field(x => x.GameState);
            AddNavigationField(
                name: "gameDate",
                resolve: context => context.Source.GameDate);
            //todo enum Field(x => x.GameState);
            AddNavigationField(
                name: "team",
                resolve: context => context.Source.Team);
        }
    }
}
