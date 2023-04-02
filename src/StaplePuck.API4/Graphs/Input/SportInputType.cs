using GraphQL.Types;

public class SportInputType : InputObjectGraphType
{
    public SportInputType()
    {
        Name = "SportInput";
        Field<NonNullGraphType<StringGraphType>>("name");
    }
}
