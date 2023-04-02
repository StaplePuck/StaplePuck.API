using GraphQL.Types;

public class PlayerInputType : InputObjectGraphType
{
    public PlayerInputType()
    {
        Name = "PlayerInput";
        Field<NonNullGraphType<StringGraphType>>("fullName");
        Field<NonNullGraphType<StringGraphType>>("externalId");
        Field<NonNullGraphType<StringGraphType>>("shortName");
        Field<NonNullGraphType<StringGraphType>>("firstName");
        Field<NonNullGraphType<StringGraphType>>("lastName");
        Field<NonNullGraphType<IntGraphType>>("number");
    }
}
