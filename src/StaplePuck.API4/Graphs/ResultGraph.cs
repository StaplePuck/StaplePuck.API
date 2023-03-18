using StaplePuck.Core.Data;
using StaplePuck.Data;

public class ResultGraph : EfObjectGraphType<StaplePuckContext, ResultModel>
{
    public ResultGraph(IEfGraphQLService<StaplePuckContext> graphQLService) : base(graphQLService)
    {
        Field(x => x.Id);
        Field(x => x.Message);
        Field(x => x.Success);
    }
}
