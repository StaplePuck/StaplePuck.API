using GraphQL.Types;

namespace StaplePuck.API.Graphs.Input
{
    public class UserInputType : InputObjectGraphType
    {
        public UserInputType()
        {
            Name = "UserInput";
            Field<StringGraphType>("name");
            Field<NonNullGraphType<StringGraphType>>("email");

            Field<BooleanGraphType>("receiveEmails");
        }
    }
}
