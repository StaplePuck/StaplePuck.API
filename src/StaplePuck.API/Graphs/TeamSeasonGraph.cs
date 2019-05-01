using GraphQL.EntityFramework;
using StaplePuck.Core.Stats;


namespace StaplePuck.API.Graphs
{
    public class TeamSeasonGraph : EfObjectGraphType<TeamSeason>
    {
        public TeamSeasonGraph(IEfGraphQLService graphQLService) : base(graphQLService)
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
