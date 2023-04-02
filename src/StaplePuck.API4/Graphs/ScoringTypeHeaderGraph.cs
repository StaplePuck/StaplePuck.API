using StaplePuck.Core.Stats;
using StaplePuck.Data;

public class ScoringTypeHeaderGraph : EfObjectGraphType<StaplePuckContext, ScoringType>
{
    public ScoringTypeHeaderGraph(IEfGraphQLService<StaplePuckContext> graphQlService) :
        base(graphQlService)
    {
        AutoMap();
    }
}

