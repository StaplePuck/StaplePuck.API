using GraphQL.EntityFramework;
using StaplePuck.Core.Fantasy;
using StaplePuck.Core.Scoring;
using StaplePuck.Core.Stats;
using StaplePuck.Data;

namespace StaplePuck.API.Graphs
{
    public class FantasyTeamPlayersGraph : EfObjectGraphType<StaplePuckContext, FantasyTeamPlayers>
    {
        public FantasyTeamPlayersGraph(IEfGraphQLService<StaplePuckContext> graphQLService) : base(graphQLService)
        {
            Field(x => x.PlayerId);
            AddNavigationField(
                name: "fantasyTeam",
                resolve: context => context.Source.FantasyTeam);
            AddNavigationField(
                name: "player",
                resolve: context => context.Source.Player);
            AddNavigationField(
                name: "playerCalculatedScore",
                resolve: context => context.Source.PlayerCalculatedScore);
            AddNavigationField(
                name: "playerSeason",
                resolve: context => context.Source.PlayerSeason);
        }
    }
}
