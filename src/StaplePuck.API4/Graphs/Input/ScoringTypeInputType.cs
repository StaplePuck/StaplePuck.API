using GraphQL.Types;

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
