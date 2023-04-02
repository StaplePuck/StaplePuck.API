using StaplePuck.Core.Fantasy;
using StaplePuck.Data;

public class LeagueMailGraph : EfObjectGraphType<StaplePuckContext, LeagueMail>
{
    public LeagueMailGraph(IEfGraphQLService<StaplePuckContext> graphQlService) :
        base(graphQlService)
    {
        AutoMap();
    }
}

