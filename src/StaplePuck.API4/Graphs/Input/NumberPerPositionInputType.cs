using GraphQL.Types;

public class NumberPerPositionInputType : InputObjectGraphType
{
    public NumberPerPositionInputType() 
    {
        Name = "NumberPerPosition";
        Field<NonNullGraphType<IntGraphType>>("positionTypeId");
        Field<NonNullGraphType<IntGraphType>>("count");
    }
}
