using GraphQL.EntityFramework;
using StaplePuck.Core.Stats;
using StaplePuck.Data;

namespace StaplePuck.API.Graphs
{
    public class TeamGraph : EfObjectGraphType<StaplePuckContext, Team>
    {
        public TeamGraph(IEfGraphQLService<StaplePuckContext> graphQLService) : base(graphQLService)
        {
            Field(x => x.Id);
            Field(x => x.ExternalId);
            Field(x => x.FullName);
            Field(x => x.Name);
            Field(x => x.ShortName);
            Field(x => x.LocationName);
        }
    }
}
