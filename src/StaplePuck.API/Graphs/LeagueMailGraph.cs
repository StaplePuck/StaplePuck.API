using GraphQL.EntityFramework;
using StaplePuck.Core.Fantasy;
using StaplePuck.Data;

namespace StaplePuck.API.Graphs
{
    public class LeagueMailGraph : EfObjectGraphType<StaplePuckContext, LeagueMail>
    {
        public LeagueMailGraph(IEfGraphQLService<StaplePuckContext> graphQLService) : base(graphQLService)
        {
            // maybe no
            Field(x => x.Id);
        }
    }
}
