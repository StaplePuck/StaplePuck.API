using StaplePuck.Core.Fantasy;
using StaplePuck.Data;

public class UserGraph : EfObjectGraphType<StaplePuckContext, User>
{
    public UserGraph(IEfGraphQLService<StaplePuckContext> graphQlService) :
        base(graphQlService)
    {
        AutoMap();
    }
}
