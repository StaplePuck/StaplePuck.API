using GraphQL.EntityFramework;
using StaplePuck.Core.Stats;


namespace StaplePuck.API.Graphs
{
    public class TeamSeasonGraph : EfObjectGraphType<TeamSeason>
    {
        public TeamSeasonGraph(IEfGraphQLService graphQLService) : base(graphQLService)
        {
            Field(x => x.TeamId);
            AddNavigationField<TeamGraph, Team>(
                name: "team",
                resolve: context => context.Source.Team);
            Field(x => x.SeasonId);
            AddNavigationField<SeasonGraph, Season>(
                name: "season",
                resolve: context => context.Source.Season);
            AddNavigationField<PlayerSeasonGraph, PlayerSeason>(
                name: "playerSeasons",
                resolve: context => context.Source.PlayerSeasons);
        }
    }
}
