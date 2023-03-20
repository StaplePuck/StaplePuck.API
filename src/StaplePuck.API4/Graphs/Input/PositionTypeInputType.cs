using GraphQL.Types;

public class PositionTypeInputType : InputObjectGraphType
{
    public PositionTypeInputType()
    {
        Name = "PostionTypeInput";
        Field<NonNullGraphType<StringGraphType>>("name");

        Field<IntGraphType>("id");
        Field<StringGraphType>("shortName");
        Field<IntGraphType>("sportId");
        Field<StringGraphType>("sport");
        Field<IntGraphType>("scoringPositions");
    }
}
