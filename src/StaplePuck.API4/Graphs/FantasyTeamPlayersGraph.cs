using StaplePuck.Core.Fantasy;
using StaplePuck.Core.Stats;
using StaplePuck.Data;

public class FantasyTeamPlayersGraph : EfObjectGraphType<StaplePuckContext, FantasyTeamPlayers>
{
    public FantasyTeamPlayersGraph(IEfGraphQLService<StaplePuckContext> graphQlService) :
        base(graphQlService)
    {
        AutoMap();
    }
}
