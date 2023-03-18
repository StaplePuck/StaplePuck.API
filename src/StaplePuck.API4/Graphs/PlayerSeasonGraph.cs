using StaplePuck.Core.Fantasy;
using StaplePuck.Core.Stats;
using StaplePuck.Data;

public class PlayerSeasonGraph : EfObjectGraphType<StaplePuckContext, PlayerSeason>
{
    public PlayerSeasonGraph(IEfGraphQLService<StaplePuckContext> graphQlService) :
        base(graphQlService)
    {
        AutoMap();
    }
}

