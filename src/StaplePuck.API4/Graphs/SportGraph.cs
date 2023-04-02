using StaplePuck.Core.Stats;
using StaplePuck.Data;

public class SportGraph : EfObjectGraphType<StaplePuckContext, Sport>
{
    public SportGraph(IEfGraphQLService<StaplePuckContext> graphQlService) :
        base(graphQlService)
    {
        AutoMap();
    }
}
