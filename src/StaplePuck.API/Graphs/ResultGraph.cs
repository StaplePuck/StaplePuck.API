using GraphQL.EntityFramework;
using StaplePuck.Core.Data;


namespace StaplePuck.API.Graphs
{
    public class ResultGraph : EfObjectGraphType<ResultModel>
    {
        public ResultGraph(IEfGraphQLService graphQLService) : base(graphQLService)
        {
            Field(x => x.Id);
            Field(x => x.Message);
            Field(x => x.Success);
        }
    }
}
