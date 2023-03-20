using GraphQL.Types;

public class GameDateInputType : InputObjectGraphType
{
    public GameDateInputType()
    {
        Name = "GameDateInput";
        Field<NonNullGraphType<StringGraphType>>("id");

        //Field<StringGraphType>("gameDateSeasons");
        //Field<StringGraphType>("teamsStateOnDate");
        //Field<StringGraphType>("playersStatsOnDate");

        Field<ListGraphType<GameDateSeasonInputType>>("gameDateSeasons");
        Field<ListGraphType<PlayerStatsOnDateInputType>>("playersStatsOnDate");
    }
}
