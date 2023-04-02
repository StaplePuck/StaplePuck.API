using GraphQL.Types;

public class FantasyTeamCreateInputType : InputObjectGraphType
{
    public FantasyTeamCreateInputType()
    {
        Name = "FantasyTeamInput";
        Field<NonNullGraphType<StringGraphType>>("name");
        Field<NonNullGraphType<IntGraphType>>("leagueId");
    }
}
