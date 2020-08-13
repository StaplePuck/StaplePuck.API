using GraphQL.EntityFramework;
using StaplePuck.Core.Scoring;
using StaplePuck.Core.Stats;
using StaplePuck.Data;

namespace StaplePuck.API.Graphs
{
    public class PlayerCalculatedScoreGraph : EfObjectGraphType<StaplePuckContext, PlayerCalculatedScore>
    {
        public PlayerCalculatedScoreGraph(IEfGraphQLService<StaplePuckContext> graphQLService) : base(graphQLService)
        {
            Field(x => x.NumberOfSelectedByTeams);
            Field(x => x.GameState);
            AddNavigationField(
                name: "player",
                resolve: context => context.Source.Player);
            Field(x => x.Score);
            AddNavigationListField(
                name: "scoring",
                resolve: context => context.Source.Scoring);
            AddNavigationField(
                name: "playerSeason",
                resolve: context => context.Source.PlayerSeason);
            
            Field(x => x.TodaysScore);
        }
    }
}
