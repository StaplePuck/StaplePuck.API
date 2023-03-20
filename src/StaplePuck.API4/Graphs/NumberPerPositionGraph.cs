using StaplePuck.Core.Fantasy;
using StaplePuck.Data;

public class NumberPerPositionGraph : EfObjectGraphType<StaplePuckContext, NumberPerPosition>
{
    public NumberPerPositionGraph(IEfGraphQLService<StaplePuckContext> graphQlService) :
        base(graphQlService)
    {
        AutoMap();
    }
}
