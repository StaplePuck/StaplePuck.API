using StaplePuck.Core.Fantasy;
using StaplePuck.Core.Stats;
using StaplePuck.Data;

public class GameDateSeasonGraph : EfObjectGraphType<StaplePuckContext, GameDateSeason>
{
    public GameDateSeasonGraph(IEfGraphQLService<StaplePuckContext> graphQlService) :
        base(graphQlService)
    {
        AutoMap();
    }
}

