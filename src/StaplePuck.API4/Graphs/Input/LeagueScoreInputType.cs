using GraphQL.Types;

public class LeagueScoreInputType : InputObjectGraphType
{
    public LeagueScoreInputType()
    {
        Name = "LeagueScoreInput";
        Field<NonNullGraphType<IntGraphType>>("id");

        Field<ListGraphType<FantasyTeamScoreInputType>>("fantasyTeams");
        Field<ListGraphType<PlayerCalculatedScoreInputType>>("playerCalculatedScores");
    }
}
