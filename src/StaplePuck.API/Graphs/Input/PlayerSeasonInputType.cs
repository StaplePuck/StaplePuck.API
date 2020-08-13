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

            Field<IntGraphType>("playerId");
            Field<IntGraphType>("seasonId");
            Field<StringGraphType>("season");
            Field<IntGraphType>("teamId");
            Field<IntGraphType>("positionTypeId");
            Field<StringGraphType>("teamSeason");
            Field<StringGraphType>("teamStateForSeason");
            Field<StringGraphType>("fantasyTeamPlayers");
            Field<StringGraphType>("playerCalculatedScores");
        }
    }
}
