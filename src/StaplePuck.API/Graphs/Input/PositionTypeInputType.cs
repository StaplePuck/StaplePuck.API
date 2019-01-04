using GraphQL.Types;

namespace StaplePuck.API.Graphs.Input
{
    public class PositionTypeInputType : InputObjectGraphType
    {
        public PositionTypeInputType()
        {
            Name = "PostionTypeInput";
            Field<NonNullGraphType<StringGraphType>>("name");
        }
    }
}
