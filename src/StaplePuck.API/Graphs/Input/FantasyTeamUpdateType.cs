using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Types;

namespace StaplePuck.API.Graphs.Input
{
    public class FantasyTeamUpdateInputType : InputObjectGraphType
    {
        public FantasyTeamUpdateInputType()
        {
            Name = "FantasyTeamUpdateInput";
            Field<NonNullGraphType<IntGraphType>>("id");

            Field<ListGraphType<FantasyTeamPlayersInputType>>("fantasyTeamPlayers");
        }
    }
}
