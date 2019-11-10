using GraphQL.EntityFramework;
using StaplePuck.Core.Stats;
using StaplePuck.Data;

namespace StaplePuck.API.Graphs
{
    public class TeamStateForSeasonGraph : EfObjectGraphType<StaplePuckContext, TeamStateForSeason>
    {
        public TeamStateForSeasonGraph(IEfGraphQLService<StaplePuckContext> graphQLService) : base(graphQLService)
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
