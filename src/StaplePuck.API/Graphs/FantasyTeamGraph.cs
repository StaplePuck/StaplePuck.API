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
            AddNavigationListField(
                name: "fantasyTeamPlayers",
                resolve: context => context.Source.FantasyTeamPlayers);
            AddNavigationField(
                name: "league",
                resolve: context => context.Source.League);
            AddNavigationField(
                name: "GM",
                resolve: context => context.Source.GM);

            Field(x => x.Rank);
            Field(x => x.Score);
            Field(x => x.TodaysScore);
        }
    }
}
