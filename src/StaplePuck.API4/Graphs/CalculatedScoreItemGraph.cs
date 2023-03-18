
using StaplePuck.Core.Fantasy;
using StaplePuck.Core.Scoring;
using StaplePuck.Data;

public class CalculatedScoreItemGraph : EfObjectGraphType<StaplePuckContext, CalculatedScoreItem>
{
    public CalculatedScoreItemGraph(IEfGraphQLService<StaplePuckContext> graphQlService) :
        base(graphQlService)
    {
        AutoMap();
    }
}

