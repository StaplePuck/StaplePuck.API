using GraphQL.Types;

public class SeasonInputType : InputObjectGraphType
{
    public SeasonInputType() 
    {
        Name = "SeasonInput";
        Field<NonNullGraphType<StringGraphType>>("fullName");
        Field<StringGraphType>("externalId");
        Field<BooleanGraphType>("isPlayoffs");
        Field<IntGraphType>("startRound");
        Field<SportInputType>("sport");
        Field<ListGraphType<PlayerSeasonInputType>>("playerSeasons");

        Field<IntGraphType>("id");
        Field<StringGraphType>("leagues");
        Field<IntGraphType>("sportId");
        Field<StringGraphType>("gameDates");
        Field<StringGraphType>("teamSeasons");
    }
}
