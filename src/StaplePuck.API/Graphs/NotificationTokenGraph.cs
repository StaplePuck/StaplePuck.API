using GraphQL.EntityFramework;
using StaplePuck.Core.Fantasy;
using StaplePuck.Data;

namespace StaplePuck.API.Graphs
{
    public class NotificationTokenGraph : EfObjectGraphType<StaplePuckContext, NotificationToken>
    {
        public NotificationTokenGraph(IEfGraphQLService<StaplePuckContext> graphQLService) : base(graphQLService)
        {
            Field(x => x.Id);
            Field(x => x.Token);
            Field(x => x.UserId);
            AddNavigationField(
                name: "user",
                resolve: context => context.Source.User);
        }
    }
}
