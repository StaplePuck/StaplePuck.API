using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Types;

namespace StaplePuck.API.Graphs.Input
{
    public class LeagueScoreInputType : InputObjectGraphType
    {
        public LeagueScoreInputType()
        {
            Name = "LeagueScoreInput";
            Field<NonNullGraphType<IntGraphType>>("id");

            Field<ListGraphType<FantasyTeamScoreInputType>>("fantasyTeams");
            Field<ListGraphType<PlayerCalculatedScoreInputType>>("playerCalculatedScores");
        }
    }
}
