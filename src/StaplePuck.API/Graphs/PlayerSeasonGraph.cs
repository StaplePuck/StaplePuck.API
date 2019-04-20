using GraphQL.EntityFramework;
using StaplePuck.Core.Stats;

namespace StaplePuck.API.Graphs
{
    public class PlayerSeasonGraph : EfObjectGraphType<PlayerSeason>
    {
        public PlayerSeasonGraph(IEfGraphQLService graphQLService) : base(graphQLService)
        {
            Field(x => x.PlayerId);
            Field(x => x.PositionTypeId);
            Field(x => x.TeamId);
            Field(x => x.SeasonId);
            AddNavigationField<PlayerGraph, Player>(
                name: "player",
                resolve: context => context.Source.Player);
            AddNavigationField<SeasonGraph, Season>(
                name: "season",
                resolve: context => context.Source.Season);
            AddNavigationField<TeamGraph, Team>(
                name: "team",
                resolve: context => context.Source.Team);
            AddNavigationField<PositionTypeGraph, PositionType>(
                name: "positionType",
                resolve: context => context.Source.PositionType);
        }
    }
}
