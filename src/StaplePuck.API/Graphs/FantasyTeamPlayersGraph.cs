using GraphQL.EntityFramework;
using StaplePuck.Core.Fantasy;
using StaplePuck.Core.Stats;

namespace StaplePuck.API.Graphs
{
    public class FantasyTeamPlayersGraph : EfObjectGraphType<FantasyTeamPlayers>
    {
        public FantasyTeamPlayersGraph(IEfGraphQLService graphQLService) : base(graphQLService)
        {
            AddNavigationField<FantasyTeamGraph, FantasyTeam>(
                name: "fantasyTeam",
                resolve: context => context.Source.FantasyTeam);
            AddNavigationField<PlayerGraph, Player>(
                name: "player",
                resolve: context => context.Source.Player);
        }
    }
}
