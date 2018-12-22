using GraphQL.EntityFramework;
using StaplePuck.Core.Scoring;

namespace StaplePuck.API.Graphs
{
    public class LeagueScoreGraph : EfObjectGraphType<LeagueScore>
    {
        public LeagueScoreGraph(IEfGraphQLService graphQLService) : base(graphQLService)
        {
            // maybe no
            Field(x => x.Date);
        }
    }
}
