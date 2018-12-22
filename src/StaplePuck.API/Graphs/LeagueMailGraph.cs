using GraphQL.EntityFramework;
using StaplePuck.Core.Fantasy;

namespace StaplePuck.API.Graphs
{
    public class LeagueMailGraph : EfObjectGraphType<LeagueMail>
    {
        public LeagueMailGraph(IEfGraphQLService graphQLService) : base(graphQLService)
        {
            // maybe no
            Field(x => x.Id);
        }
    }
}
