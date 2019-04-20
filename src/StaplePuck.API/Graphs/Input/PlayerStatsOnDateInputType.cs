using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Types;

namespace StaplePuck.API.Graphs.Input
{
    public class PlayerStatsOnDateInputType : InputObjectGraphType
    {
        public PlayerStatsOnDateInputType()
        {
            Name = "PlayerStatsOnDateInput";
            Field<NonNullGraphType<PlayerStatInputType>>("player");
            Field<ListGraphType<PlayerScoreInputType>>("playerScores");

            Field<IntGraphType>("id");
            Field<IntGraphType>("playerId");
            Field<StringGraphType>("gameDateId");
            Field<StringGraphType>("gameDate");
        }
    }
}