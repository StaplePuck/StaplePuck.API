using StaplePuck.Core.Fantasy;
using StaplePuck.Core.Stats;
using StaplePuck.Data;

public class PlayerStatsOnDateGraph : EfObjectGraphType<StaplePuckContext, PlayerStatsOnDate>
{
    public PlayerStatsOnDateGraph(IEfGraphQLService<StaplePuckContext> graphQlService) :
        base(graphQlService)
    {
        AutoMap();
    }
}

