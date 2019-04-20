using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Types;

namespace StaplePuck.API.Graphs.Input
{
    public class PlayerStatInputType : InputObjectGraphType
    {
        public PlayerStatInputType()
        {
            Name = "PlayerStatInput";
            Field<NonNullGraphType<StringGraphType>>("externalId");


            Field<IntGraphType>("id");
            Field<StringGraphType>("fullName");
            Field<StringGraphType>("shortName");
            Field<StringGraphType>("firstName");
            Field<StringGraphType>("lastName");
            Field<StringGraphType>("playerSeasons");
            Field<IntGraphType>("number");
            Field<StringGraphType>("fantasyTeamPlayers");
            Field<StringGraphType>("statsOnDate");
            Field<IntGraphType>("sportId");
            Field<StringGraphType>("sport");
        }
    }
}
