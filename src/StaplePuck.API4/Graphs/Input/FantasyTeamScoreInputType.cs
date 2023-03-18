using GraphQL.Types;

public class FantasyTeamScoreInputType : InputObjectGraphType
{
    public FantasyTeamScoreInputType()
    {
        Name = "FantasyTeamScoreInput";
        Field<NonNullGraphType<IntGraphType>>("id");
        Field<NonNullGraphType<IntGraphType>>("rank");
        Field<NonNullGraphType<IntGraphType>>("score");
        Field<NonNullGraphType<IntGraphType>>("todaysScore");
    }
}

