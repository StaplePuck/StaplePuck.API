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
            AddNavigationField(
                name: "player",
                resolve: context => context.Source.Player);
            AddNavigationField(
                name: "season",
                resolve: context => context.Source.Season);
            AddNavigationField(
                name: "team",
                resolve: context => context.Source.Team);
            AddNavigationField(
                name: "positionType",
                resolve: context => context.Source.PositionType);
        }
    }
}
