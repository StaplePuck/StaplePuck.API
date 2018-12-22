using GraphQL.EntityFramework;
using StaplePuck.Core.Fantasy;
using StaplePuck.Core.Scoring;

namespace StaplePuck.API.Graphs
{
    public class TeamScoreGraph : EfObjectGraphType<TeamScore>
    {
        public TeamScoreGraph(IEfGraphQLService graphQLService) : base(graphQLService)
        {
            Field(x => x.Date);
            AddNavigationField<FantasyTeamGraph, FantasyTeam>(
                name: "fantasyTeam",
                resolve: context => context.Source.FantasyTeam);
            AddNavigationField<PlayerCalculatedScoreGraph, PlayerCalculatedScore>(
                name: "players",
                resolve: context => context.Source.Players);
            Field(x => x.Rank);
            Field(x => x.Score);
            Field(x => x.TodaysScore);
        }
    }
}
