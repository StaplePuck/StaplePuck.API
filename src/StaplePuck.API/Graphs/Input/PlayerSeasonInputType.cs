using GraphQL.Types;

namespace StaplePuck.API.Graphs.Input
{
    public class PlayerSeasonInputType : InputObjectGraphType
    {
        public PlayerSeasonInputType()
        {
            Name = "PlayerSeasonInput";
            Field<NonNullGraphType<PlayerInputType>>("player");
            Field<NonNullGraphType<TeamInputType>>("team");
            Field<NonNullGraphType<PositionTypeInputType>>("positionType");
        }
    }
}
