using StaplePuck.Core.Fantasy;
using StaplePuck.Core.Stats;
using StaplePuck.Data;

public class PlayerGraph : EfObjectGraphType<StaplePuckContext, Player>
{
    public PlayerGraph(IEfGraphQLService<StaplePuckContext> graphQlService) :
        base(graphQlService)
    {
        AutoMap();
    }
}

