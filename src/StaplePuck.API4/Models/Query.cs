using GraphQL.MicrosoftDI;
using GraphQL.Types;
using Microsoft.Extensions.Options;
using StaplePuck.Core.Auth;
using StaplePuck.Core.Models;
using StaplePuck.Data;
using StaplePuck.Data.Repositories;

public class Query :
    QueryGraphType<StaplePuckContext>
{
    public Query(IFantasyRepository fantasyRepository, IEfGraphQLService<StaplePuckContext> efGraphQlService, IOptions<Auth0APISettings> options) :
        base(efGraphQlService)
    {
        AddSingleField(name: "currentUser2",
            resolve: context =>
            {
                var dataContext = context.DbContext;
                var user = ((GraphQLUserContext)context.UserContext).User;
                if (user.Claims.Count() == 0)
                {
                    throw new Exception("Not authenticated");
                }
                var subject = user.GetUserId(options.Value);
                return dataContext.Users.Where(x => x.ExternalId == subject);
            }).Authorize();

        AddQueryField(
            name: "fantasyTeams",
            resolve: context =>
            {
                return context.DbContext.FantasyTeams;
            });

        AddQueryField(
            name: "leagues",
            resolve: context => context.DbContext.Leagues
            );

        AddQueryField(
            name: "players",
            resolve: context => context.DbContext.Players
            );

        AddQueryField(
            name: "playerSeasons",
            resolve: context => context.DbContext.PlayerSeasons
            );

        AddQueryField(
            name: "scoringTypes",
            resolve: context => context.DbContext.ScoringTypes
            );

        AddQueryField(
            name: "seasons",
            resolve: context => context.DbContext.Seasons);

        AddQueryField(
            name: "sports",
            resolve: context => context.DbContext.Sports
            );

        AddQueryField(
            name: "teams",
            resolve: context => context.DbContext.Teams
            );

        AddQueryField(
            name: "users",
            resolve: context => context.DbContext.Users
            );

        AddQueryField(
            name: "playerCalculatedScores",
            resolve: context => context.DbContext.PlayerCalculatedScores
            );

        Field<ListGraphType<ScoringTypeHeaderGraph>>("scoringTypeHeaders")
            .Argument<IntGraphType>("id")
            .Resolve(context =>
            {
                var id = context.GetArgument<int>("id");
                var dbContext = ResolveDbContext(context);
                var league = dbContext.Leagues.Include(x => x.ScoringRules).ThenInclude(x => x.ScoringType).First(x => x.Id == id);
                var scoring = league.ScoringRules.Where(x => x.ScoringWeight != 0).Select(x => x.ScoringType);
                return scoring.Distinct();
            });

        Field<ListGraphType<ScoringTypeHeaderGraph>>("scoringTypeHeadersForTeam")
            .Argument<IntGraphType>("id")
            .Resolve(context =>
            {
                var id = context.GetArgument<int>("id");
                var dbContext = ResolveDbContext(context);
                var team = dbContext.FantasyTeams.Include(x => x.League).ThenInclude(x => x!.ScoringRules).ThenInclude(x => x.ScoringType).First(x => x.Id == id);
                if (team.League == null)
                {
                    throw new Exception();
                }
                var scoring = team.League.ScoringRules.Where(x => x.ScoringWeight != 0).Select(x => x.ScoringType);
                return scoring.Distinct().OrderBy(x => x!.Id);
            });

        Field<LeagueGraph>("leagueScores")
            .Argument<IntGraphType>("id")
            .Resolve(context =>
            {
                var id = context.GetArgument<int>("id");
                var dbContext = ResolveDbContext(context);
                var league = dbContext.Leagues.Include(x => x.FantasyTeams).ThenInclude(x => x.GM).FirstOrDefault(x => x.Id == id);
                if (league != null)
                {
                    if (league.IsLocked)
                    {
                        foreach (var fteam in league.FantasyTeams.Where(x => !x.IsPaid))
                        {
                            fteam.Score = -1;
                            fteam.TodaysScore = -1;
                        }
                    }
                }

                return league;
            });

        Field<ListGraphType<FantasyTeamGraph>>("fantasyTeamsNotPaid")
            .Argument<IntGraphType>("id")
            .Resolve(context =>
            {
                var id = context.GetArgument<int>("id");
                var dbContext = ResolveDbContext(context);
                var league = dbContext.Leagues.Include(x => x.FantasyTeams).ThenInclude(x => x.GM).FirstOrDefault(x => x.Id == id);
                if (league == null || !league.IsLocked)
                {
                    return null;
                }
                foreach (var fteam in league.FantasyTeams.Where(x => !x.IsPaid))
                {
                    fteam.Score = -1;
                    fteam.TodaysScore = -1;
                }
                return league.FantasyTeams.Where(x => !x.IsPaid);
            });

        Field<ListGraphType<ResultGraph>>("fantasyTeamValidation")
            .Argument<IntGraphType>("id")
            .Resolve()
            .WithScope()
            .WithService<StaplePuckContext>()
            .ResolveAsync(async (context, db) =>
            {
                var id = context.GetArgument<int>("id");
                var dbContext = ResolveDbContext(context);
                var fantasyTeam = dbContext.FantasyTeams.Include(x => x.FantasyTeamPlayers).FirstOrDefault(x => x.Id == id);
                if (fantasyTeam == null)
                {
                    return null;
                }
                var validation = await fantasyRepository.Validate(dbContext, fantasyTeam);
                var list = new List<ResultModel>();
                foreach (var item in validation)
                {
                    list.Add(new ResultModel { Message = item, Success = false });
                }
                return list;
            });

        Field<ListGraphType<PlayerCalculatedScoreGraph>>("playerCalculatedScoresForTeam")
            .Argument<IdGraphType>("leagueId")
            .Argument<IdGraphType>("teamId")
            .Resolve(context =>
            {

                var leagueId = context.GetArgument<int>("leagueId");
                var teamId = context.GetArgument<int>("teamId");
                var dbContext = ResolveDbContext(context);
                var pcs = dbContext.PlayerCalculatedScores
                    .Include(x => x.PlayerSeason)
                    .ThenInclude(x => x!.PositionType)

                    .Include(x => x.PlayerSeason)
                    .ThenInclude(x => x!.Team)

                    .Include(x => x.PlayerSeason)
                    .ThenInclude(x => x!.TeamStateForSeason)

                    .Include(x => x.Player)

                    .Include(x => x.League)

                    .Include(x => x.Scoring)
                    .ThenInclude(x => x.ScoringType)
                    .Where(x => x.LeagueId == leagueId);
                if (pcs == null)
                {
                    return null;
                }
                if (teamId > 0)
                {
                    return pcs.Where(x => x.PlayerSeason != null && x.PlayerSeason.TeamId == teamId);
                }
                return pcs;
            });

        AddQueryField("myFantasyTeams",
            resolve: context =>
            {
                var leagueId = context.GetArgument<int>("leagueId");
                var dbContext = ResolveDbContext(context);
                var user = ((GraphQLUserContext)context.UserContext).User;
                if (user.Claims.Count() == 0)
                {
                    return dbContext.FantasyTeams.Where(x => false);
                }
                var subject = user.GetUserId(options.Value);

                if (leagueId > 0)
                {
                    return dbContext.FantasyTeams.Where(x => x.GM != null && x.GM.ExternalId == subject && x.LeagueId == leagueId);
                }
                return dbContext.FantasyTeams.Where(x => x.GM != null && x.GM.ExternalId == subject).Include(x => x.League);
            }
            ).Argument<IdGraphType>("leagueId");

        Field<UserGraph>("currentUser")
            .Resolve(context =>
            {
                var dbContext = ResolveDbContext(context);
                var user = ((GraphQLUserContext)context.UserContext).User;
                if (user.Claims.Count() == 0)
                {
                    throw new Exception("Not authenticated");
                }
                var subject = user.GetUserId(options.Value);
                var users = dbContext.Users.Where(x => x.ExternalId == subject).Include(x => x.NotificationTokens);
                return users.FirstOrDefault();
            }).Authorize();
    }
}