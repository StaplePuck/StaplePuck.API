using GraphQL.Types;

public class GameDateSeasonInputType : InputObjectGraphType
{
    public GameDateSeasonInputType()
    {
        Name = "GameDateSeasonInput";
        Field<NonNullGraphType<SeasonStatInputType>>("season");

        Field<StringGraphType>("gameDateId");
        Field<StringGraphType>("gameDate");
        Field<IntGraphType>("seasonId");
    }
}
