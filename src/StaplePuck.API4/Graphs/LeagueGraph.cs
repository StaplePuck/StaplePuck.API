using StaplePuck.Core.Fantasy;
using StaplePuck.Data;

public class LeagueGraph : EfObjectGraphType<StaplePuckContext, League>
{
    public LeagueGraph(IEfGraphQLService<StaplePuckContext> graphQlService) :
        base(graphQlService)
    {
        AutoMap();
    }
}

