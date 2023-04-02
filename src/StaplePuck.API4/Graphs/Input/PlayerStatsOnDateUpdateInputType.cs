using GraphQL.Types;

public class PlayerStatsOnDateUpdateInputType : InputObjectGraphType
{
    public PlayerStatsOnDateUpdateInputType() 
    {
        Name = "PlayerStatsOnDateUpdateInput";
        Field<ListGraphType<PlayerScoreUpdateInputType>>("playerScores");
        Field<NonNullGraphType<IntGraphType>>("playerId");
        Field<NonNullGraphType<StringGraphType>>("gameDateId");

        Field<PlayerStatInputType>("player");
        Field<IntGraphType>("id");
        Field<StringGraphType>("gameDate");
    }
}
