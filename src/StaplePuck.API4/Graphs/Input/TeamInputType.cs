﻿using GraphQL.Types;

public class TeamInputType : InputObjectGraphType
{
    public TeamInputType() 
    {
        Name = "TeamInput";
        Field<NonNullGraphType<StringGraphType>>("name");
        Field<NonNullGraphType<StringGraphType>>("fullName");
        Field<NonNullGraphType<StringGraphType>>("shortName");
        Field<NonNullGraphType<IntGraphType>>("externalId");
        Field<NonNullGraphType<StringGraphType>>("locationName");
    }
}
