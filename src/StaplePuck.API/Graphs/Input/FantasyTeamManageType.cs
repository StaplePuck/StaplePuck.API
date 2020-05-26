using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Types;

namespace StaplePuck.API.Graphs.Input
{
    public class FantasyTeamManageType : InputObjectGraphType
    {
        public FantasyTeamManageType()
        {
            Name = "FantasyTeamManageInput";
            Field<NonNullGraphType<IntGraphType>>("id");
            Field<NonNullGraphType<BooleanGraphType>>("isPaid");
        }
    }
}
