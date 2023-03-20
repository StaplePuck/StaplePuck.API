using GraphQL.EntityFramework;
using StaplePuck.Core.Stats;
using StaplePuck.Data;


namespace StaplePuck.API.Graphs
{
    public class TeamSeasonGraph : EfObjectGraphType<StaplePuckContext, TeamSeason>
    {
        public TeamSeasonGraph(IEfGraphQLService<StaplePuckContext> graphQLService) : base(graphQLService)
        {
            Field(x => x.TeamId);
            AddNavigationField(
                name: "team",
                resolve: context => context.Source.Team);
            Field(x => x.SeasonId);
            AddNavigationField(
                name: "season",
                resolve: context => context.Source.Season);
            AddNavigationListField(
                name: "playerSeasons",
                resolve: context => context.Source.PlayerSeasons);
        }
    }
}
