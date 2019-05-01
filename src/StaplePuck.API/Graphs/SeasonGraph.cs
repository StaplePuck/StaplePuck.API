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
            AddNavigationListField(
                name: "gameDates",
                resolve: context => context.Source.GameDates);
            AddNavigationListField(
                name: "playerSeasons",
                resolve: context => context.Source.PlayerSeasons);
            AddNavigationListField(
                name: "teamSeasons",
                resolve: context => context.Source.TeamSeasons);
            Field(x => x.IsPlayoffs);
            AddNavigationListField(
                name: "leagues",
                resolve: context => context.Source.Leagues);
            AddNavigationField(
                name: "sport",
                resolve: context => context.Source.Sport);
            Field(x => x.StartRound);

        }
    }
}
