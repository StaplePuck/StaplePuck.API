using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Types;


namespace StaplePuck.API.Graphs.Input
{
    public class TeamStatInputType : InputObjectGraphType
    {
        public TeamStatInputType()
        {
            Name = "TeamStatInput";
            Field<NonNullGraphType<IntGraphType>>("externalId");

            Field<StringGraphType>("name");
            Field<StringGraphType>("fullName");
            Field<StringGraphType>("shortName");
            Field<StringGraphType>("locationName");

            Field<IntGraphType>("id");
            Field<StringGraphType>("teamStateOnDates");
        }
    }
}
