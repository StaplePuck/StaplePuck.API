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
            Field<NonNullGraphType<IntGraphType>>("PositionTypeId");
            Field<NonNullGraphType<IntGraphType>>("ScoringTypeId");
            Field<NonNullGraphType<IntGraphType>>("LeagueId");

            Field<NonNullGraphType<IntGraphType>>("PointsPerScore");
        }
    }
}
