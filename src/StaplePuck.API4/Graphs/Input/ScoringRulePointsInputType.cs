using GraphQL.Types;

public class ScoringRulePointsInputType : InputObjectGraphType
{
    public ScoringRulePointsInputType() 
    {
        Name = "ScoringRulePointsInput";
        Field<NonNullGraphType<IntGraphType>>("positionTypeId");
        Field<NonNullGraphType<IntGraphType>>("scoringTypeId");
        Field<NonNullGraphType<FloatGraphType>>("scoringWeight");
    }
}
