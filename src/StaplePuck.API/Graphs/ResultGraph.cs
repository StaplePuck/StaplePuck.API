using GraphQL.EntityFramework;
using StaplePuck.Core.Data;
using StaplePuck.Data;

namespace StaplePuck.API.Graphs
{
    public class ResultGraph : EfObjectGraphType<StaplePuckContext, ResultModel>
    {
        public ResultGraph(IEfGraphQLService<StaplePuckContext> graphQLService) : base(graphQLService)
        {
            Field(x => x.Id);
            Field(x => x.Message);
            Field(x => x.Success);
        }
    }
}
