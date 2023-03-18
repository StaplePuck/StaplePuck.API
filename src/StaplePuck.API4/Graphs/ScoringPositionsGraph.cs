using StaplePuck.Core.Fantasy;
using StaplePuck.Core.Stats;
using StaplePuck.Data;

public class ScoringPositionsGraph : EfObjectGraphType<StaplePuckContext, ScoringPositions>
{
    public ScoringPositionsGraph(IEfGraphQLService<StaplePuckContext> graphQlService) :
        base(graphQlService)
    {
        AutoMap();
    }
}

