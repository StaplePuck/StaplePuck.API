using GraphQL.EntityFramework;
using StaplePuck.Core.Fantasy;
using StaplePuck.Core.Stats;

namespace StaplePuck.API.Graphs
{
    public class SeasonGraph : EfObjectGraphType<Season>
    {
        public SeasonGraph(IEfGraphQLService graphQLService) : base(graphQLService)
        {
            Field(x => x.Id);
            Field(x => x.ExternalId);
            Field(x => x.FullName);
            AddNavigationField<GameDateSeasonGraph, GameDateSeason>(
                name: "gameDates",
                resolve: context => context.Source.GameDates);
            AddNavigationField<PlayerSeasonGraph, PlayerSeason>(
                name: "playerSeasons",
                resolve: context => context.Source.PlayerSeasons);
            Field(x => x.IsPlayoffs);
            AddNavigationField<LeagueGraph, League>(
                name: "leagues",
                resolve: context => context.Source.Leagues);
            AddNavigationField<SportGraph, Sport>(
                name: "sport",
                resolve: context => context.Source.Sport);
            Field(x => x.StartRound);

        }
    }
}
