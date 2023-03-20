using GraphQL.Types;

public class PlayerScoreUpdateInputType : InputObjectGraphType
{
    public PlayerScoreUpdateInputType()
    {
        Name = "PlayerScoreUpdateInput";

        Field<NonNullGraphType<IntGraphType>>("total");
        Field<NonNullGraphType<IntGraphType>>("scoringTypeId");
        Field<NonNullGraphType<BooleanGraphType>>("adminOverride");

        Field<IntGraphType>("id");
        Field<ScoringTypeInputType>("scoringType");
        Field<IntGraphType>("playerStatsOnDateId");
        Field<StringGraphType>("playerStatsOnDate");
    }
}
