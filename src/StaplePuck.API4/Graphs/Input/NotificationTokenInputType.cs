using GraphQL.Types;

public class NotificationTokenInputType : InputObjectGraphType
{
    public NotificationTokenInputType()
    {
        Name = "NotificationTokenInput";
        Field<StringGraphType>("token");
    }
}
