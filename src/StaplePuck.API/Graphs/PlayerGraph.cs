using GraphQL.EntityFramework;
using StaplePuck.Core.Fantasy;
using StaplePuck.Core.Stats;
using StaplePuck.Data;

namespace StaplePuck.API.Graphs
{
    public class PlayerGraph : EfObjectGraphType<StaplePuckContext, Player>
    {
        public PlayerGraph(IEfGraphQLService<StaplePuckContext> graphQLService) : base(graphQLService)
        {
            Field(x => x.Id);
            Field(x => x.ExternalId);
            AddNavigationListField(
                name: "fantasyTeamPlayers",
                resolve: context => context.Source.FantasyTeamPlayers);
            Field(x => x.FirstName);
            Field(x => x.FullName);
            //Field(x => x.FullNameWithPosition);
            Field(x => x.LastName);
            Field(x => x.Number);
            AddNavigationListField(
                name: "playerSeasons",
                resolve: context => context.Source.PlayerSeasons);
            // todo....
            //AddNavigationField<PositionTypeGraph, PositionType>(
            //    name: "position",
            //    resolve: context => context.Source.Position);
            Field(x => x.ShortName);
            //Field(x => x.StatsOnDate);
        }
    }
}
