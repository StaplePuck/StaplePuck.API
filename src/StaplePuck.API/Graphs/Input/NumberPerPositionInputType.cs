using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Types;

namespace StaplePuck.API.Graphs.Input
{
    public class NumberPerPositionInputType : InputObjectGraphType
    {
        public NumberPerPositionInputType()
        {
            Name = "NumberPerPosition";
            Field<NonNullGraphType<IntGraphType>>("positionTypeId");
            Field<NonNullGraphType<IntGraphType>>("count");
        }
    }
}
