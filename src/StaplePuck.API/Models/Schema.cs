using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Utilities;
using StaplePuck.Core.Data;
using StaplePuck.Core.Fantasy;
using StaplePuck.Core.Scoring;
using StaplePuck.Core.Stats;

namespace StaplePuck.API.Models
{
    public class Schema : GraphQL.Types.Schema
    {
        public Schema(IServiceProvider serviceProvider, Query query, Mutation mutation) : base(serviceProvider)
        {
            RegisterTypeMapping(typeof(CalculatedScoreItem), typeof(Graphs.CalculatedScoreItemGraph));
            RegisterTypeMapping(typeof(FantasyTeam), typeof(Graphs.FantasyTeamGraph));
            RegisterTypeMapping(typeof(FantasyTeamPlayers), typeof(Graphs.FantasyTeamPlayersGraph));
            RegisterTypeMapping(typeof(GameDate), typeof(Graphs.GameDateGraph));
            RegisterTypeMapping(typeof(GameDateSeason), typeof(Graphs.GameDateSeasonGraph));
            RegisterTypeMapping(typeof(League), typeof(Graphs.LeagueGraph));
            RegisterTypeMapping(typeof(LeagueMail), typeof(Graphs.LeagueMailGraph));
            RegisterTypeMapping(typeof(NotificationToken), typeof(Graphs.NotificationTokenGraph));
            RegisterTypeMapping(typeof(NumberPerPosition), typeof(Graphs.NumberPerPositionGraph));
            RegisterTypeMapping(typeof(PlayerCalculatedScore), typeof(Graphs.PlayerCalculatedScoreGraph));
            RegisterTypeMapping(typeof(Player), typeof(Graphs.PlayerGraph));
            RegisterTypeMapping(typeof(PlayerCalculatedScore), typeof(Graphs.PlayerCalculatedScoreGraph));
            RegisterTypeMapping(typeof(PlayerScore), typeof(Graphs.PlayerScoreGraph));
            RegisterTypeMapping(typeof(PlayerSeason), typeof(Graphs.PlayerSeasonGraph));
            RegisterTypeMapping(typeof(PlayerStatsOnDate), typeof(Graphs.PlayerStatsOnDateGraph));
            RegisterTypeMapping(typeof(PositionType), typeof(Graphs.PositionTypeGraph));
            RegisterTypeMapping(typeof(ResultModel), typeof(Graphs.ResultGraph));
            RegisterTypeMapping(typeof(ScoringPositions), typeof(Graphs.ScoringPositionsGraph));
            RegisterTypeMapping(typeof(ScoringRulePoints), typeof(Graphs.ScoringRulePointsGraph));
            RegisterTypeMapping(typeof(ScoringType), typeof(Graphs.ScoringTypeGraph));
            RegisterTypeMapping(typeof(Season), typeof(Graphs.SeasonGraph));
            RegisterTypeMapping(typeof(Sport), typeof(Graphs.SportGraph));
            RegisterTypeMapping(typeof(Team), typeof(Graphs.TeamGraph));
            RegisterTypeMapping(typeof(TeamSeason), typeof(Graphs.TeamSeasonGraph));
            RegisterTypeMapping(typeof(TeamStateForSeason), typeof(Graphs.TeamStateForSeasonGraph));
            RegisterTypeMapping(typeof(User), typeof(Graphs.UserGraph));

            Query = query;
            Mutation = mutation;
        }
    }
}
