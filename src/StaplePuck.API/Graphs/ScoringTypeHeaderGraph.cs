using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StaplePuck.Core.Fantasy;
using StaplePuck.Core.Stats;
using StaplePuck.Data;
using GraphQL.EntityFramework;

namespace StaplePuck.API.Graphs
{
    public class ScoringTypeHeaderGraph : EfObjectGraphType<StaplePuckContext, ScoringType>
    {
        public ScoringTypeHeaderGraph(IEfGraphQLService<StaplePuckContext> graphQlService) : base(graphQlService)
        {
            Field(x => x.Id);
            Field(x => x.Name);
            Field(x => x.ShortName);
        }
    }
}
