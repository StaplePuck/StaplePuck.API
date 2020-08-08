using GraphQL.EntityFramework;
using StaplePuck.Core.Fantasy;
using StaplePuck.Data;

namespace StaplePuck.API.Graphs
{
    public class UserGraph : EfObjectGraphType<StaplePuckContext, User>
    {
        public UserGraph(IEfGraphQLService<StaplePuckContext> graphQLService) : base(graphQLService)
        {
            Field(x => x.Id);
            // auth?
            Field(x => x.ReceiveEmails);
            Field(x => x.ReceiveNotifications);
            Field(x => x.ReceiveNegativeNotifications);
            Field(x => x.Email);
            AddNavigationListField(
                name: "fantasyTeams",
                resolve: context => context.Source.FantasyTeams);
            Field(x => x.Name);
            Field(x => x.ExternalId);
            AddNavigationListField(
                name: "notificationTokens",
                resolve: context => context.Source.NotificationTokens);
        }
    }
}
