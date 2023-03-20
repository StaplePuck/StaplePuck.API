using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Types;


namespace StaplePuck.API.Graphs.Input
{
    public class FantasyTeamCreateInputType : InputObjectGraphType
    {
        public FantasyTeamCreateInputType()
        {
            Name = "FantasyTeamInput";
            Field<NonNullGraphType<StringGraphType>>("name");
            Field<NonNullGraphType<IntGraphType>>("leagueId");
        }
    }
}
