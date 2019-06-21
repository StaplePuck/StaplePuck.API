using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Types;

namespace StaplePuck.API.Graphs.Input
{
    public class GameDateInputType : InputObjectGraphType
    {
        public GameDateInputType()
        {
            Name = "GameDateInput";
            Field<NonNullGraphType<StringGraphType>>("id");

            //Field<StringGraphType>("gameDateSeasons");
            //Field<StringGraphType>("teamsStateOnDate");
            //Field<StringGraphType>("playersStatsOnDate");

            Field<ListGraphType<GameDateSeasonInputType>>("gameDateSeasons");
            Field<ListGraphType<PlayerStatsOnDateInputType>>("playersStatsOnDate");
        }
    }
}
