using GraphQL.Types;

public class FantasyTeamPlayersInputType : InputObjectGraphType
{
    public FantasyTeamPlayersInputType()
    {
        Name = "FantasyTeamPlayersInput";
        Field<NonNullGraphType<IntGraphType>>("playerId");
    }
}
