using GraphQL.Types;

public class FantasyTeamUpdateType : InputObjectGraphType
{
    public FantasyTeamUpdateType()
    {
        Name = "FantasyTeamUpdateInput";
        Field<NonNullGraphType<IntGraphType>>("id");

        Field<ListGraphType<FantasyTeamPlayersInputType>>("fantasyTeamPlayers");

        Field<StringGraphType>("name");
    }
}
