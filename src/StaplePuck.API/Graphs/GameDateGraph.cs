using GraphQL.EntityFramework;
using StaplePuck.Core.Stats;

namespace StaplePuck.API.Graphs
{
    public class GameDateGraph : EfObjectGraphType<GameDate>
    {
        public GameDateGraph(IEfGraphQLService graphQLService) : base(graphQLService)
        {
            Field(x => x.Id);
            AddNavigationField<GameDateSeasonGraph, GameDateSeason>(
                name: "gameDateSeason",
                resolve: context => context.Source.GameDateSeasons);
            AddNavigationField<PlayerStatsOnDateGraph, PlayerStatsOnDate>(
                name: "playersStatsOnDate",
                resolve: context => context.Source.PlayersStatsOnDate);
            AddNavigationField<TeamStateOnDateGraph, TeamStateOnDate>(
                name: "teamsStateOnDate",
                resolve: context => context.Source.TeamsStateOnDate);
        }
    }
}
