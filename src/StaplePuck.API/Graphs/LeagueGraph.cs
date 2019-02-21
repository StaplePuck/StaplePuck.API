using GraphQL.EntityFramework;
using StaplePuck.Core.Fantasy;
using StaplePuck.Core.Stats;

namespace StaplePuck.API.Graphs
{
    public class LeagueGraph : EfObjectGraphType<League>
    {
        public LeagueGraph(IEfGraphQLService graphQLService) : base(graphQLService)
        {
            Field(x => x.Id);
            Field(x => x.AllowMultipleTeams);
            Field(x => x.Announcement);
            AddNavigationField<UserGraph, User>(
                name: "commissioner",
                resolve: context => context.Source.Commissioner);
            Field(x => x.Description);
            AddNavigationField<FantasyTeamGraph, FantasyTeam>(
                name: "fantasyTeams",
                resolve: context => context.Source.FantasyTeams);
            Field(x => x.IsLocked);
            Field(x => x.Name);
            //Field(x => x.LeagueMails);
            AddNavigationField<NumberPerPositionGraph, NumberPerPosition>(
                name: "numberPerPositions",
                resolve: context => context.Source.NumberPerPositions);
            //Field(x => x.Password);
            Field(x => x.PaymentInfo);
            Field(x => x.PlayersPerTeam);
            AddNavigationField<ScoringRulePointsGraph, ScoringRulePoints>(
                name: "scoringRules",
                resolve: context => context.Source.ScoringRules);
            AddNavigationField<SeasonGraph, Season>(
                name: "season",
                resolve: context => context.Source.Season);
        }
    }
}
