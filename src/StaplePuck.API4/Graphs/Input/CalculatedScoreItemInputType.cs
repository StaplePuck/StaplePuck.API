using GraphQL.Types;

public class CalculatedScoreItemInputType : InputObjectGraphType
{
    public CalculatedScoreItemInputType()
    {
        Name = "CalculatedScoreItemInput";

        Field<NonNullGraphType<IntGraphType>>("scoringTypeId");
        Field<NonNullGraphType<IntGraphType>>("total");
        Field<NonNullGraphType<IntGraphType>>("todaysTotal");
        Field<NonNullGraphType<IntGraphType>>("score");
        Field<NonNullGraphType<IntGraphType>>("todaysScore");

        Field<IntGraphType>("id");
    }
}
