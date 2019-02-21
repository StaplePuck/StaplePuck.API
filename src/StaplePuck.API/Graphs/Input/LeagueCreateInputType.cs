using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Types;

namespace StaplePuck.API.Graphs.Input
{
    public class LeagueCreateInputType : InputObjectGraphType
    {
        public LeagueCreateInputType()
        {
            Name = "LeagueInput";
            Field<NonNullGraphType<StringGraphType>>("name");
            Field<NonNullGraphType<IntGraphType>>("seasonId");
            Field<NonNullGraphType<IntGraphType>>("commissionerId");
        }
    }
}
