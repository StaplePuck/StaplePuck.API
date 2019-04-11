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
            //Field<NonNullGraphType<IntGraphType>>("leagueId");

            //GameDateSeasons { get; set; }
        //public List<TeamStateOnDate> TeamsStateOnDate { get; set; }
        //public List<PlayerStatsOnDate> PlayersStatsOnDate
        }
    }
}
