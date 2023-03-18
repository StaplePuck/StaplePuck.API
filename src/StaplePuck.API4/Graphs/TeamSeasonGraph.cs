using StaplePuck.Core.Fantasy;
using StaplePuck.Core.Stats;
using StaplePuck.Data;

public class TeamSeasonGraph : EfObjectGraphType<StaplePuckContext, TeamSeason>
{
    public TeamSeasonGraph(IEfGraphQLService<StaplePuckContext> graphQlService) :
        base(graphQlService)
    {
        AutoMap();
    }
}

