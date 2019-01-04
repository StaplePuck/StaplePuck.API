using GraphQL.Types;

namespace StaplePuck.API.Graphs.Input
{
    public class SportInputType : InputObjectGraphType
    {
        public SportInputType()
        {
            Name = "SportInput";
            Field<NonNullGraphType<StringGraphType>>("name");
        }
    }
}
