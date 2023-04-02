using StaplePuck.Core.Fantasy;
using StaplePuck.Core.Stats;
using StaplePuck.Data;

public class TeamStateForSeasonGraph : EfObjectGraphType<StaplePuckContext, TeamStateForSeason>
{
    public TeamStateForSeasonGraph(IEfGraphQLService<StaplePuckContext> graphQlService) :
        base(graphQlService)
    {
        AutoMap();
    }
}
