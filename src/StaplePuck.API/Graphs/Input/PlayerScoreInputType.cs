using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Types;

namespace StaplePuck.API.Graphs.Input
{
    public class PlayerScoreInputType : InputObjectGraphType
    {
        public PlayerScoreInputType()
        {
            Name = "PlayerScoreInput";

            Field<NonNullGraphType<IntGraphType>>("total");
            Field<NonNullGraphType<ScoringTypeInputType>>("scoringType");
            Field<NonNullGraphType<BooleanGraphType>>("adminOverride");

            Field<IntGraphType>("id");
            Field<IntGraphType>("playerStatsOnDateId");
            Field<StringGraphType>("playerStatsOnDate");
            Field<IntGraphType>("scoringTypeId");
        }
    }
}
