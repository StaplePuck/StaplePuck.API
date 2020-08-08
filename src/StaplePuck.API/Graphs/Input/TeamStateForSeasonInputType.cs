using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Types;


namespace StaplePuck.API.Graphs.Input
{
    public class TeamStateForSeasonInputType : InputObjectGraphType
    {
        public TeamStateForSeasonInputType()
        {
            Name = "TeamStateForSeasonInput";
            Field<NonNullGraphType<TeamStatInputType>>("team");
            Field<NonNullGraphType<SeasonStatInputType>>("season");
            Field<NonNullGraphType<IntGraphType>>("gameState");

            Field<IntGraphType>("teamId");
            Field<IntGraphType>("seasonId");
            Field<StringGraphType>("playerSeasons");
        }
    }
}
