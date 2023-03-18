using GraphQL.Types;

public class ScoringRulePointsInputType : InputObjectGraphType
{
    public ScoringRulePointsInputType() 
    {
        Name = "ScoringRulePointsInput";
        Field<NonNullGraphType<IntGraphType>>("positionTypeId");
        Field<NonNullGraphType<IntGraphType>>("scoringTypeId");
        //Field<NonNullGraphType<IntGraphType>>("LeagueId");

        Field<NonNullGraphType<IntGraphType>>("pointsPerScore");
        Field<NonNullGraphType<FloatGraphType>>("scoringWeight");
    }
}
