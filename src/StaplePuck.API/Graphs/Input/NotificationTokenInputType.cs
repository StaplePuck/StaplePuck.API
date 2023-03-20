using GraphQL.Types;

namespace StaplePuck.API.Graphs.Input
{
    public class NotificationTokenInputType : InputObjectGraphType
    {
        public NotificationTokenInputType()
        {
            Name = "NotificationTokenInput";
            Field<StringGraphType>("token");
        }
    }
}
