using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Types;

namespace StaplePuck.API.Graphs.Input
{
    public class ScoringRulePointsInputType : InputObjectGraphType
    {
        public ScoringRulePointsInputType()
        {
            Name = "ScoringRulePointsInput";
            Field<NonNullGraphType<IntGraphType>>("positionTypeId");
            Field<NonNullGraphType<IntGraphType>>("scoringTypeId");
            //Field<NonNullGraphType<IntGraphType>>("LeagueId");

            Field<NonNullGraphType<IntGraphType>>("pointsPerScore");
        }
    }
}
