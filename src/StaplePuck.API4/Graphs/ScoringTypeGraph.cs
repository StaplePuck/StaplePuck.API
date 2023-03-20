using StaplePuck.Core.Fantasy;
using StaplePuck.Core.Stats;
using StaplePuck.Data;

public class ScoringTypeGraph : EfObjectGraphType<StaplePuckContext, ScoringType>
{
    public ScoringTypeGraph(IEfGraphQLService<StaplePuckContext> graphQlService) :
        base(graphQlService)
    {
        AutoMap();
    }
}

