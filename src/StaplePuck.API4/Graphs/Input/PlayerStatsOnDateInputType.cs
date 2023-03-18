using GraphQL.Types;

public class PlayerStatsOnDateInputType : InputObjectGraphType
{
    public PlayerStatsOnDateInputType() 
    {
        Name = "PlayerStatsOnDateInput";
        Field<NonNullGraphType<PlayerStatInputType>>("player");
        Field<ListGraphType<PlayerScoreInputType>>("playerScores");

        Field<IntGraphType>("id");
        Field<IntGraphType>("playerId");
        Field<StringGraphType>("gameDateId");
        Field<StringGraphType>("gameDate");
    }
}
