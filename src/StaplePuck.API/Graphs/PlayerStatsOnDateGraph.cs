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
            AddNavigationField(
                name: "gameDate",
                resolve: context => context.Source.GameDate);
            AddNavigationField(
                name: "player",
                resolve: context => context.Source.Player);
            AddNavigationListField(
                name: "playerScores",
                resolve: context => context.Source.PlayerScores);
        }
    }
}
