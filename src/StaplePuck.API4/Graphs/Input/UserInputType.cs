using GraphQL.Types;

public class UserInputType : InputObjectGraphType
{
    public UserInputType() 
    {
        Name = "UserInput";
        Field<StringGraphType>("name");
        Field<NonNullGraphType<StringGraphType>>("email");

        Field<BooleanGraphType>("receiveEmails");
        Field<BooleanGraphType>("receiveNotifications");
        Field<BooleanGraphType>("receiveNegativeNotifications");
    }
}
