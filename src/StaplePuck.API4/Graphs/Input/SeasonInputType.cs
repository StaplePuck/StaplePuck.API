using GraphQL.Types;

public class SeasonInputType : InputObjectGraphType
{
    public SeasonInputType() 
    {
        Name = "SeasonInput";
        Field<NonNullGraphType<StringGraphType>>("fullName");
        Field<StringGraphType>("externalId");
        Field<StringGraphType>("externalPlayerUrl");
        Field<StringGraphType>("externalPlayerUrl2");
        Field<BooleanGraphType>("isPlayoffs");
        Field<IntGraphType>("startRound");
        Field<SportInputType>("sport");
        Field<ListGraphType<PlayerSeasonInputType>>("playerSeasons");
    }
}
