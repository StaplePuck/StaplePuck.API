using GraphQL.Types;

namespace StaplePuck.API.Graphs.Input
{
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

            Field<IntGraphType>("id");
            Field<StringGraphType>("playerSeasons");
            Field<StringGraphType>("fantasyTeamPlayers");
            Field<StringGraphType>("statsOnDate");
            Field<IntGraphType>("SportId");
            Field<StringGraphType>("sport");
        }
    }
}
