using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Types;

namespace StaplePuck.API.Graphs.Input
{
    public class FantasyTeamScoreInputType : InputObjectGraphType
    {
        public FantasyTeamScoreInputType()
        {
            Name = "FantasyTeamScoreInput";
            Field<NonNullGraphType<IntGraphType>>("id");
            Field<NonNullGraphType<IntGraphType>>("rank");
            Field<NonNullGraphType<IntGraphType>>("score");
            Field<NonNullGraphType<IntGraphType>>("todaysScore");
        }
    }
}
