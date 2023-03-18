using GraphQL.Types;

public class TeamStateForSeasonInputType : InputObjectGraphType
{
    public TeamStateForSeasonInputType() 
    {
        Name = "TeamStateForSeasonInput";
        Field<NonNullGraphType<TeamStatInputType>>("team");
        Field<NonNullGraphType<SeasonStatInputType>>("season");
        Field<NonNullGraphType<IntGraphType>>("gameState");

        Field<IntGraphType>("teamId");
        Field<IntGraphType>("seasonId");
        Field<StringGraphType>("playerSeasons");
    }
}
