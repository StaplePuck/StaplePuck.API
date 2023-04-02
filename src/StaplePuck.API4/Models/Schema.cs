using StaplePuck.Core.Fantasy;
using StaplePuck.Core.Scoring;
using StaplePuck.Core.Stats;

public class Schema :
    GraphQL.Types.Schema
{
    public Schema(IServiceProvider provider, Query query, Mutation mutation) :
        base(provider)
    {
        RegisterTypeMapping(typeof(CalculatedScoreItem), typeof(CalculatedScoreItemGraph));
        RegisterTypeMapping(typeof(FantasyTeam), typeof(FantasyTeamGraph));
        RegisterTypeMapping(typeof(FantasyTeamPlayers), typeof(FantasyTeamPlayersGraph));
        RegisterTypeMapping(typeof(GameDate), typeof(GameDateGraph));
        RegisterTypeMapping(typeof(GameDateSeason), typeof(GameDateSeasonGraph));
        RegisterTypeMapping(typeof(League), typeof(LeagueGraph));
        RegisterTypeMapping(typeof(LeagueMail), typeof(LeagueMailGraph));
        RegisterTypeMapping(typeof(NotificationToken), typeof(NotificationTokenGraph));
        RegisterTypeMapping(typeof(NumberPerPosition), typeof(NumberPerPositionGraph));
        RegisterTypeMapping(typeof(PlayerCalculatedScore), typeof(PlayerCalculatedScoreGraph));
        RegisterTypeMapping(typeof(Player), typeof(PlayerGraph));
        RegisterTypeMapping(typeof(PlayerScore), typeof(PlayerScoreGraph));
        RegisterTypeMapping(typeof(PlayerSeason), typeof(PlayerSeasonGraph));
        RegisterTypeMapping(typeof(PlayerStatsOnDate), typeof(PlayerStatsOnDateGraph));
        RegisterTypeMapping(typeof(PositionType), typeof(PositionTypeGraph));
        RegisterTypeMapping(typeof(ScoringPositions), typeof(ScoringPositionsGraph));
        RegisterTypeMapping(typeof(ScoringRulePoints), typeof(ScoringRulePointsGraph));
        RegisterTypeMapping(typeof(ScoringType), typeof(ScoringTypeGraph));
        RegisterTypeMapping(typeof(Season), typeof(SeasonGraph));
        RegisterTypeMapping(typeof(Sport), typeof(SportGraph));
        RegisterTypeMapping(typeof(Team), typeof(TeamGraph));
        RegisterTypeMapping(typeof(TeamSeason), typeof(TeamSeasonGraph));
        RegisterTypeMapping(typeof(TeamStateForSeason), typeof(TeamStateForSeasonGraph));
        RegisterTypeMapping(typeof(User), typeof(UserGraph));


        //RegisterTypeMapping(typeof(EmployeeSummary), typeof(EmployeeSummaryGraphType));
        //RegisterTypeMapping(typeof(Company), typeof(CompanyGraphType));
        Mutation = mutation;
        Query = query;
        //   Subscription = (Subscription)provider.GetService(typeof(Subscription));
    }
}