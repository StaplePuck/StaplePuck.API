﻿using GraphQL.Types;

public class LeagueUpdateInputType : InputObjectGraphType
{
    public LeagueUpdateInputType() 
    {
        Name = "LeagueUpdateInput";
        Field<NonNullGraphType<IntGraphType>>("id");

        Field<StringGraphType>("name");
        Field<StringGraphType>("announcement");
        Field<StringGraphType>("description");
        Field<BooleanGraphType>("isActive");
        Field<BooleanGraphType>("isLocked");
        Field<StringGraphType>("paymentInfo");
        Field<BooleanGraphType>("allowMultipleTeams");
        Field<IntGraphType>("playersPerTeam");

        Field<ListGraphType<ScoringRulePointsInputType>>("scoringRules");
        Field<ListGraphType<NumberPerPositionInputType>>("numberPerPositions");
        Field<ListGraphType<FantasyTeamManageInputType>>("fantasyTeams");
    }
}
