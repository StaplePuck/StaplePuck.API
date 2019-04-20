using GraphQL.EntityFramework;
using StaplePuck.Core.Stats;

namespace StaplePuck.API.Graphs
{
    public class PlayerStatsOnDateGraph : EfObjectGraphType<PlayerStatsOnDate>
    {
        public PlayerStatsOnDateGraph(IEfGraphQLService graphQLService) : base(graphQLService)
        {
            Field(x => x.Id);
            Field(x => x.PlayerId);
            AddNavigationField<GameDateGraph, GameDate>(
                name: "gameDate",
                resolve: context => context.Source.GameDate);
            AddNavigationField<PlayerGraph, Player>(
                name: "player",
                resolve: context => context.Source.Player);
            AddNavigationField<PlayerScoreGraph, PlayerScore>(
                name: "playerScores",
                resolve: context => context.Source.PlayerScores);
        }
    }
}
