using StaplePuck.Core.Fantasy;
using StaplePuck.Core.Stats;
using StaplePuck.Data;

public class PositionTypeGraph : EfObjectGraphType<StaplePuckContext, PositionType>
{
    public PositionTypeGraph(IEfGraphQLService<StaplePuckContext> graphQlService) :
        base(graphQlService)
    {
        AutoMap();
    }
}

