using StaplePuck.Core.Fantasy;
using StaplePuck.Data;

public class ScoringRulePointsGraph : EfObjectGraphType<StaplePuckContext, ScoringRulePoints>
{
    public ScoringRulePointsGraph(IEfGraphQLService<StaplePuckContext> graphQlService) :
        base(graphQlService)
    {
        AutoMap();
    }
}

