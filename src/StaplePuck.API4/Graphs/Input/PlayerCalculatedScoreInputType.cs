using GraphQL.Types;

public class PlayerCalculatedScoreInputType : InputObjectGraphType
{
    public PlayerCalculatedScoreInputType() 
    {
        Name = "PlayerCalculatedScoreInput";
        Field<NonNullGraphType<IntGraphType>>("playerId");
        Field<NonNullGraphType<IntGraphType>>("leagueId");

        Field<ListGraphType<CalculatedScoreItemInputType>>("scoring");

        Field<NonNullGraphType<IntGraphType>>("numberOfSelectedByTeams");
        Field<NonNullGraphType<IntGraphType>>("gameState");
        Field<NonNullGraphType<IntGraphType>>("score");
        Field<NonNullGraphType<IntGraphType>>("todaysScore");
        Field<StringGraphType>("playerSeason");
    }
}
