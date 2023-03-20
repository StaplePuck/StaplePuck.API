using GraphQL.EntityFramework;
using StaplePuck.Core.Stats;
using StaplePuck.Data;

namespace StaplePuck.API.Graphs
{
    public class GameDateGraph : EfObjectGraphType<StaplePuckContext, GameDate>
    {
        public GameDateGraph(IEfGraphQLService<StaplePuckContext> graphQLService) : base(graphQLService)
        {
            Field(x => x.Id);
            AddNavigationListField(
                name: "gameDateSeason",
                resolve: context => context.Source.GameDateSeasons);
            AddNavigationListField(
                name: "playersStatsOnDate",
                resolve: context => context.Source.PlayersStatsOnDate);
        }
    }
}
