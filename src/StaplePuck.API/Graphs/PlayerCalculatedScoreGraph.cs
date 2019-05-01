using GraphQL.EntityFramework;
using StaplePuck.Core.Scoring;
using StaplePuck.Core.Stats;

namespace StaplePuck.API.Graphs
{
    public class PlayerCalculatedScoreGraph : EfObjectGraphType<PlayerCalculatedScore>
    {
        public PlayerCalculatedScoreGraph(IEfGraphQLService graphQLService) : base(graphQLService)
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
            
            Field(x => x.TodaysScore);
        }
    }
}
