using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Types;

namespace StaplePuck.API.Graphs.Input
{
    public class ScoringTypeInputType : InputObjectGraphType
    {
        public ScoringTypeInputType()
        {
            Name = "ScoringTypeInput";
            Field<NonNullGraphType<StringGraphType>>("name");

            Field<IntGraphType>("id");
            Field<StringGraphType>("shortName");
            Field<StringGraphType>("scoringPositions");
            Field<IntGraphType>("sportId");
            Field<StringGraphType>("sport");
        }
    }
}
