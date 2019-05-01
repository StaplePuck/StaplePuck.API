using GraphQL.EntityFramework;
using StaplePuck.Core.Stats;

namespace StaplePuck.API.Graphs
{
    public class GameDateGraph : EfObjectGraphType<GameDate>
    {
        public GameDateGraph(IEfGraphQLService graphQLService) : base(graphQLService)
        {
            Field(x => x.Id);
            AddNavigationListField(
                name: "gameDateSeason",
                resolve: context => context.Source.GameDateSeasons);
            AddNavigationListField(
                name: "playersStatsOnDate",
                resolve: context => context.Source.PlayersStatsOnDate);
            AddNavigationListField(
                name: "teamsStateOnDate",
                resolve: context => context.Source.TeamsStateOnDate);
        }
    }
}
