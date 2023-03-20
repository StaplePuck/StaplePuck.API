using GraphQL.EntityFramework;
using StaplePuck.Core.Stats;
using StaplePuck.Data;

namespace StaplePuck.API.Graphs
{
    public class GameDateSeasonGraph : EfObjectGraphType<StaplePuckContext, GameDateSeason>
    {
        public GameDateSeasonGraph(IEfGraphQLService<StaplePuckContext> graphQLService) : base(graphQLService)
        {
            // no
            Field(x => x.GameDateId);
            AddNavigationField(
                name: "gameDate",
                resolve: context => context.Source.GameDate);
            AddNavigationField(
                name: "season",
                resolve: context => context.Source.Season);
        }
    }
}
