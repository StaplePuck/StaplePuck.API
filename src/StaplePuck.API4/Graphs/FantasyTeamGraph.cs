
using StaplePuck.Core.Fantasy;
using StaplePuck.Data;

public class FantasyTeamGraph : EfObjectGraphType<StaplePuckContext, FantasyTeam>
{
    public FantasyTeamGraph(IEfGraphQLService<StaplePuckContext> graphQlService) :
        base(graphQlService)
    {
        AutoMap();
    }
}
