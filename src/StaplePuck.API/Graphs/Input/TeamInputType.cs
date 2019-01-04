using GraphQL.Types;

namespace StaplePuck.API.Graphs.Input
{
    public class TeamInputType : InputObjectGraphType
    {
        public TeamInputType()
        {
            Name = "TeamInput";
            Field<NonNullGraphType<StringGraphType>>("name");
            Field<NonNullGraphType<StringGraphType>>("shortName");
            Field<NonNullGraphType<StringGraphType>>("externalId");
        }
    }
}
