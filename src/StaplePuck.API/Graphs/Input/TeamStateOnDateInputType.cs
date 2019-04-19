using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Types;


namespace StaplePuck.API.Graphs.Input
{
    public class TeamStateOnDateInputType : InputObjectGraphType
    {
        public TeamStateOnDateInputType()
        {
            Name = "TeamStateOnDateInput";
            Field<NonNullGraphType<TeamStatInputType>>("team");
            Field<NonNullGraphType<IntGraphType>>("gameState");

            Field<IntGraphType>("id");
            Field<IntGraphType>("teamId");
            Field<StringGraphType>("gameDateId");
            Field<StringGraphType>("gameDate");
        }
    }
}
