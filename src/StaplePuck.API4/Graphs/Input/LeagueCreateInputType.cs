using GraphQL.Types;

public class LeagueCreateInputType : InputObjectGraphType
{
    public LeagueCreateInputType()
    {
        Name = "LeagueInput";
        Field<NonNullGraphType<StringGraphType>>("name");
        Field<NonNullGraphType<IntGraphType>>("seasonId");
        Field<NonNullGraphType<IntGraphType>>("commissionerId");
    }
}
