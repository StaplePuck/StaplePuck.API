using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Types;


namespace StaplePuck.API.Graphs.Input
{
    public class FantasyTeamPlayersInputType : InputObjectGraphType
    {
        public FantasyTeamPlayersInputType()
        {
            Name = "FantasyTeamPlayersInput";
            Field<NonNullGraphType<IntGraphType>>("playerId");
        }
    }
}
