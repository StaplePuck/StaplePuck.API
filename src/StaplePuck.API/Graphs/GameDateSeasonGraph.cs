using GraphQL.EntityFramework;
using StaplePuck.Core.Stats;

namespace StaplePuck.API.Graphs
{
    public class GameDateSeasonGraph : EfObjectGraphType<GameDateSeason>
    {
        public GameDateSeasonGraph(IEfGraphQLService graphQLService) : base(graphQLService)
        {
            // no
            Field(x => x.GameDateId);
            AddNavigationField<GameDateGraph, GameDate>(
                name: "gameDate",
                resolve: context => context.Source.GameDate);
            AddNavigationField<SeasonGraph, Season>(
                name: "season",
                resolve: context => context.Source.Season);
        }
    }
}
