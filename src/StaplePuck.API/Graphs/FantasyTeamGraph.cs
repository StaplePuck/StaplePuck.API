using GraphQL.EntityFramework;
using StaplePuck.Core.Fantasy;

namespace StaplePuck.API.Graphs
{
    public class FantasyTeamGraph : EfObjectGraphType<FantasyTeam>
    {
        public FantasyTeamGraph(IEfGraphQLService graphQLService) : base(graphQLService)
        {
            Field(x => x.Id);
            Field(x => x.Name);
            Field(x => x.IsPaid);
            Field(x => x.IsValid);
            AddNavigationField<FantasyTeamPlayersGraph, FantasyTeamPlayers>(
                name: "fantasyTeamPlayers",
                resolve: context => context.Source.FantasyTeamPlayers);
            AddNavigationField<LeagueGraph, League>(
                name: "league",
                resolve: context => context.Source.League);
            AddNavigationField<UserGraph, User>(
                name: "GM",
                resolve: context => context.Source.GM);

            Field(x => x.Rank);
            Field(x => x.Score);
            Field(x => x.TodaysScore);
        }
    }
}
