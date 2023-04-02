using GraphQL.Types;

public class PositionTypeInputType : InputObjectGraphType
{
    public PositionTypeInputType()
    {
        Name = "PostionTypeInput";
        Field<NonNullGraphType<StringGraphType>>("name");
    }
}
