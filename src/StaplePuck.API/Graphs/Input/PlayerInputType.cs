using GraphQL.Types;

namespace StaplePuck.API.Graphs.Input
{
    public class PlayerInputType : InputObjectGraphType
    {
        public PlayerInputType()
        {
            Name = "PLayerInput";
            Field<NonNullGraphType<StringGraphType>>("fullName");
            Field<NonNullGraphType<StringGraphType>>("externalId");
            Field<NonNullGraphType<StringGraphType>>("shortName");
            Field<NonNullGraphType<StringGraphType>>("firstName");
            Field<NonNullGraphType<StringGraphType>>("lastName");
            Field<NonNullGraphType<IntGraphType>>("number");
        }
    }
}
