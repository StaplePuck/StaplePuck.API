using GraphQL.EntityFramework;
using StaplePuck.Core.Stats;

namespace StaplePuck.API.Graphs
{
    public class TeamGraph : EfObjectGraphType<Team>
    {
        public TeamGraph(IEfGraphQLService graphQLService) : base(graphQLService)
        {
            Field(x => x.Id);
            Field(x => x.ExternalId);
            Field(x => x.FullName);
            Field(x => x.Name);
            Field(x => x.ShortName);
            Field(x => x.LocationName);
            AddNavigationField<TeamStateOnDateGraph, TeamStateOnDate>(
                name: "teamStateOnDates",
                resolve: context => context.Source.TeamStateOnDates);
        }
    }
}
