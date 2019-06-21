using GraphQL.EntityFramework;
using StaplePuck.Core.Stats;

namespace StaplePuck.API.Graphs
{
    public class TeamStateForSeasonGraph : EfObjectGraphType<TeamStateForSeason>
    {
        public TeamStateForSeasonGraph(IEfGraphQLService graphQLService) : base(graphQLService)
        {
            Field(x => x.SeasonId);
            Field(x => x.TeamId);
            Field(x => x.GameState);
            AddNavigationField(
                name: "team",
                resolve: context => context.Source.Team);
        }
    }
}
