using GraphQL.EntityFramework;
using StaplePuck.Core.Fantasy;
using StaplePuck.Core.Stats;
using StaplePuck.Core.Scoring;
using StaplePuck.Data;

namespace StaplePuck.API.Graphs
{
    public class LeagueGraph : EfObjectGraphType<StaplePuckContext, League>
    {
        public LeagueGraph(IEfGraphQLService<StaplePuckContext> graphQLService) : base(graphQLService)
        {
            Field(x => x.Id);
            Field(x => x.AllowMultipleTeams);
            Field(x => x.Announcement, nullable: true);
            AddNavigationField(
                name: "commissioner",
                resolve: context => context.Source.Commissioner);
            Field(x => x.Description, nullable: true);
            AddNavigationListField(
                name: "fantasyTeams",
                resolve: context => context.Source.FantasyTeams);
            Field(x => x.IsLocked);
            Field(x => x.IsActive);
            Field(x => x.Name);
            //Field(x => x.LeagueMails);
            AddNavigationListField(
                name: "numberPerPositions",
                resolve: context => context.Source.NumberPerPositions);
            //Field(x => x.Password);
            Field(x => x.PaymentInfo, nullable: true).DefaultValue(string.Empty);
            Field(x => x.PlayersPerTeam);
            AddNavigationListField(
                name: "scoringRules",
                resolve: context => context.Source.ScoringRules);
            AddNavigationField(
                name: "season",
                resolve: context => context.Source.Season);

            AddNavigationListField(
                name: "playerCalculatedScores",
                resolve: context => context.Source.PlayerCalculatedScores);
        }
    }
}
