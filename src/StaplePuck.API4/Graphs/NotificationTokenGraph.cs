using StaplePuck.Core.Fantasy;
using StaplePuck.Data;

public class NotificationTokenGraph : EfObjectGraphType<StaplePuckContext, NotificationToken>
{
    public NotificationTokenGraph(IEfGraphQLService<StaplePuckContext> graphQlService) :
        base(graphQlService)
    {
        AutoMap();
    }
}

