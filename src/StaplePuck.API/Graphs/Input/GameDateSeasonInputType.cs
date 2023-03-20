using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Types;

namespace StaplePuck.API.Graphs.Input
{
    public class GameDateSeasonInputType : InputObjectGraphType
    {
        public GameDateSeasonInputType()
        {
            Name = "GameDateSeasonInput";
            Field<NonNullGraphType<SeasonStatInputType>>("season");

            Field<StringGraphType>("gameDateId");
            Field<StringGraphType>("gameDate");
            Field<IntGraphType>("seasonId");
        }
    }
}
