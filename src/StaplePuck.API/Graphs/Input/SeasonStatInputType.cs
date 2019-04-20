using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Types;

namespace StaplePuck.API.Graphs.Input
{
    public class SeasonStatInputType : InputObjectGraphType
    {
        public SeasonStatInputType()
        {
            Name = "SeasonStatInput";
            Field<NonNullGraphType<StringGraphType>>("externalId");

            Field<StringGraphType>("fullName");
            Field<BooleanGraphType>("isPlayoffs");
            Field<IntGraphType>("startRound");
            Field<SportInputType>("sport");
            Field<ListGraphType<PlayerSeasonInputType>>("playerSeasons");

            Field<IntGraphType>("id");
            Field<StringGraphType>("leagues");
            Field<IntGraphType>("sportId");
            Field<StringGraphType>("gameDates");
            Field<StringGraphType>("teamSeasons");
        }
    }
}
