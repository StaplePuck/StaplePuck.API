using StaplePuck.Core.Fantasy;
using StaplePuck.Core.Stats;
using StaplePuck.Data;

public class PlayerScoreGraph : EfObjectGraphType<StaplePuckContext, PlayerScore>
{
    public PlayerScoreGraph(IEfGraphQLService<StaplePuckContext> graphQlService) :
        base(graphQlService)
    {
        AutoMap();
    }
}

