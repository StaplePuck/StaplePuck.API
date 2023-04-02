using StaplePuck.Core.Fantasy;
using StaplePuck.Core.Scoring;
using StaplePuck.Data;

public class PlayerCalculatedScoreGraph : EfObjectGraphType<StaplePuckContext, PlayerCalculatedScore>
{
    public PlayerCalculatedScoreGraph(IEfGraphQLService<StaplePuckContext> graphQlService) :
        base(graphQlService)
    {
        AutoMap();
    }
}

