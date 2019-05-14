using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Types;

namespace StaplePuck.API.Graphs.Input
{
    public class PlayerScoreUpdateInputType : InputObjectGraphType
    {
        public PlayerScoreUpdateInputType()
        {
            Name = "PlayerScoreUpdateInput";

            Field<NonNullGraphType<IntGraphType>>("total");
            Field<NonNullGraphType<IntGraphType>>("scoringTypeId");
            Field<NonNullGraphType<BooleanGraphType>>("adminOverride");

            Field<IntGraphType>("id");
            Field<ScoringTypeInputType>("scoringType");
            Field<IntGraphType>("playerStatsOnDateId");
            Field<StringGraphType>("playerStatsOnDate");
        }
    }
}
