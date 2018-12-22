using GraphQL.EntityFramework;
using StaplePuck.Core.Fantasy;

namespace StaplePuck.API.Graphs
{
    public class UserGraph : EfObjectGraphType<User>
    {
        public UserGraph(IEfGraphQLService graphQLService) : base(graphQLService)
        {
            Field(x => x.Id);
            // auth?
            Field(x => x.Email);
            Field(x => x.FantasyTeams);
            Field(x => x.Name);
            Field(x => x.Email);
        }
    }
}
