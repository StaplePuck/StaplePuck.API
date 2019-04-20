using GraphQL.EntityFramework;
using StaplePuck.Core.Fantasy;
using StaplePuck.Core.Scoring;
using StaplePuck.Core.Stats;

namespace StaplePuck.API.Graphs
{
    public class FantasyTeamPlayersGraph : EfObjectGraphType<FantasyTeamPlayers>
    {
        public FantasyTeamPlayersGraph(IEfGraphQLService graphQLService) : base(graphQLService)
        {
            Field(x => x.PlayerId);
            AddNavigationField<FantasyTeamGraph, FantasyTeam>(
                name: "fantasyTeam",
                resolve: context => context.Source.FantasyTeam);
            AddNavigationField<PlayerGraph, Player>(
                name: "player",
                resolve: context => context.Source.Player);
            AddNavigationField<PlayerCalculatedScoreGraph, PlayerCalculatedScore>(
                name: "playerCalculatedScore",
                resolve: context => context.Source.PlayerCalculatedScore);
        }
    }
}
