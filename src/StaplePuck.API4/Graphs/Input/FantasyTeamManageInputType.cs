using GraphQL.Types;

public class FantasyTeamManageInputType : InputObjectGraphType
{
    public FantasyTeamManageInputType()
    {
        Name = "FantasyTeamManageInput";
        Field<NonNullGraphType<IntGraphType>>("id");
        Field<NonNullGraphType<BooleanGraphType>>("isPaid");
    }
}
