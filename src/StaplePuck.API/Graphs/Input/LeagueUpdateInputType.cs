using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Types;

namespace StaplePuck.API.Graphs.Input
{
    public class LeagueUpdateInputType : InputObjectGraphType
    {
        public LeagueUpdateInputType()
        {
            Name = "LeagueUpdate";
            Field<NonNullGraphType<IntGraphType>>("id");

            Field<StringGraphType>("name");
            Field<StringGraphType>("announcement");
            Field<StringGraphType>("description");
            Field<BooleanGraphType>("isLocked");
            Field<StringGraphType>("paymentInfo");
            Field<BooleanGraphType>("allowMultipleTeams");
            Field<IntGraphType>("playersPerTeam");

            Field<ListGraphType<ScoringRulePointsInputType>>("scoringRules");
            Field<ListGraphType<NumberPerPositionInputType>>("numberPerPositions");
        }
    }
}
