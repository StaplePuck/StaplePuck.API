using GraphQL.Types;

public class TeamStatInputType : InputObjectGraphType
{
    public TeamStatInputType() 
    {
        Name = "TeamStatInput";
        Field<NonNullGraphType<IntGraphType>>("externalId");

        Field<StringGraphType>("name");
        Field<StringGraphType>("fullName");
        Field<StringGraphType>("shortName");
        Field<StringGraphType>("locationName");

        Field<IntGraphType>("id");
        Field<StringGraphType>("teamStateOnDates");
    }
}
