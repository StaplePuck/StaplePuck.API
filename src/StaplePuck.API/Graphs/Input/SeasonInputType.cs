using GraphQL.Types;

namespace StaplePuck.API.Graphs.Input
{
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
        }
    }
}
