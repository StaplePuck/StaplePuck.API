using GraphQL.EntityFramework;
using StaplePuck.Core.Stats;
using StaplePuck.Data;

namespace StaplePuck.API.Graphs
{
    public class PlayerStatsOnDateGraph : EfObjectGraphType<StaplePuckContext, PlayerStatsOnDate>
    {
        public PlayerStatsOnDateGraph(IEfGraphQLService<StaplePuckContext> graphQLService) : base(graphQLService)
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
