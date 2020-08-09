using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StaplePuck.Core;
using StaplePuck.Core.Auth;
using StaplePuck.Core.Data;
using StaplePuck.Core.Fantasy;
using StaplePuck.Core.Stats;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace StaplePuck.Data.Repositories
{
    public class FantasyRepository : IFantasyRepository
    {
        private readonly StaplePuckContext _db;
        private readonly IAuthorizationClient _authorizationClient;
        private readonly ILogger _logger;

        public FantasyRepository(StaplePuckContext db, IAuthorizationClient authorizationClient, ILogger<IFantasyRepository> logger)
        {
            _db = db;
            _authorizationClient = authorizationClient;
            _logger = logger;
        }

        public async Task<ResultModel> Add(League league)
        {
            await _db.Leagues.AddAsync(league);
            await _db.SaveChangesAsync();

            var newLeague = await _db.Leagues.Include(l => l.Commissioner).FirstOrDefaultAsync(x => x.Id == league.Id);
            var extId = newLeague.Commissioner.ExternalId;
            await _authorizationClient.AssignUserAsCommissioner(extId, league.Id);;

            _logger.LogInformation($"Added league. Name: {league.Name} Id: new{newLeague.Id}. Commosioner ID: {extId}");

            return new ResultModel { Id = league.Id, Message = "Success", Success = true };
        }

        public async Task<ResultModel> Update(League league)
        {
            var leagueInfo = await _db.Leagues
                .Include(x => x.NumberPerPositions)
                .Include(x => x.ScoringRules)
                .Include(x => x.FantasyTeams)
                .FirstOrDefaultAsync(x => x.Id == league.Id);

            leagueInfo.AllowMultipleTeams = league.AllowMultipleTeams;
            if (league.Announcement != null)
            {
                _logger.LogInformation($"Updating league {league.Id}. Announcement: {league.Announcement}");
                leagueInfo.Announcement = league.Announcement;
            }
            if (league.Description != null)
            {
                leagueInfo.Description = league.Description;
                _logger.LogInformation($"Updating league {league.Id}. Description: {league.Description}");
            }
            leagueInfo.IsLocked = league.IsLocked;
            if (league.Name != null)
            {
                leagueInfo.Name = league.Name;
                _logger.LogInformation($"Updating league {league.Id}. Name: {league.Name}");
            }
            if (league.PaymentInfo != null)
            {
                leagueInfo.PaymentInfo = league.PaymentInfo;
                _logger.LogInformation($"Updating league {league.Id}. PaymentInfo: {league.PaymentInfo}");
            }
            if (league.PlayersPerTeam > 0)
            {
                leagueInfo.PlayersPerTeam = league.PlayersPerTeam;
                _logger.LogInformation($"Updating league {league.Id}. PlayersPerTeam: {league.PlayersPerTeam}");
            }

            if (league.NumberPerPositions != null && league.NumberPerPositions.Count > 0)
            {
                var list = new List<NumberPerPosition>();
                foreach (var item in league.NumberPerPositions)
                {
                    var perPosition = new NumberPerPosition
                    {
                        Count = item.Count,
                        LeagueId = league.Id,
                        PositionTypeId = item.PositionTypeId
                    };
                    list.Add(item);
                }
                leagueInfo.NumberPerPositions = list;
            }
            if (league.ScoringRules != null && league.ScoringRules.Count > 0)
            {
                var list = new List<ScoringRulePoints>();
                foreach (var item in league.ScoringRules)
                {
                    var rules = new ScoringRulePoints
                    {
                        PointsPerScore = item.PointsPerScore,
                        LeagueId = league.Id,
                        PositionTypeId = item.PositionTypeId,
                        ScoringTypeId = item.ScoringTypeId
                    };
                    list.Add(item);
                }
                leagueInfo.ScoringRules = list;
            }

            // Update who has paid
            if (league.FantasyTeams != null && league.FantasyTeams.Count > 0)
            {
                foreach (var item in league.FantasyTeams)
                {
                    var currentTeam = leagueInfo.FantasyTeams.FirstOrDefault(x => x.Id == item.Id);
                    currentTeam.IsPaid = item.IsPaid;
                }
            }

            _db.Leagues.Update(leagueInfo);
            await _db.SaveChangesAsync();

            _logger.LogInformation($"Update league {league.Id}");
            return new ResultModel { Id = league.Id, Message = "Success", Success = true };
        }

        public async Task<ResultModel> Add(FantasyTeam team, string userExternalId)
        {
            var user = _db.Users.FirstOrDefault(x => x.ExternalId == userExternalId);
            if (user == null)
            {
                throw new Exception();
            }
            team.UserId = user.Id;
            await _db.FantasyTeams.AddAsync(team);
            await _db.SaveChangesAsync();

            await _authorizationClient.AssignUserAsGM(userExternalId, team.Id);

            _logger.LogInformation($"Added fantasy team. Name: {team.Name}. ID: {team.Id}. GM: {user.Name}");
            return new ResultModel { Id = team.Id, Message = "Success", Success = true };
        }

        public async Task<bool> ValidateUserIsAssignedGM(int teamId, string userExternalId)
        {
            var team = await _db.FantasyTeams.Include(x => x.GM).SingleOrDefaultAsync(x => x.Id == teamId);
            if (team == null)
            {
                return false;
            }
            return team.GM.ExternalId == userExternalId;
        }

        public async Task<List<string>> ValidateNew(FantasyTeam team, string userExternalId)
        {
            var errors = new List<string>();

            if ((await _db.FantasyTeams.ToListAsync()).Any(x => x.LeagueId == team.LeagueId && x.Name.Equals(team.Name.Trim(), StringComparison.CurrentCultureIgnoreCase)))
            {
                errors.Add("Team name already exists");
            }

            var leagueInfo = await _db.Leagues.Include(x => x.FantasyTeams).ThenInclude(x => x.GM).SingleOrDefaultAsync(x => x.Id == team.LeagueId);
            if (leagueInfo.IsLocked)
            {
                errors.Add("League is currently locked");
            }
            if (!leagueInfo.AllowMultipleTeams && (leagueInfo.FantasyTeams.Count(x => x.GM.ExternalId == userExternalId) > 0))
            {
                errors.Add("This league only allows one team per user");
            }

            return errors;
        }

        public async Task<ResultModel> Update(FantasyTeam team, bool isValid)
        {
            var currentTeam = await _db.FantasyTeams.Include(x => x.League).FirstOrDefaultAsync(x => x.Id == team.Id);
            if (currentTeam?.League == null || currentTeam.League.IsLocked)
            {
                _logger.LogWarning($"League is locked and someone is trying to update fantasy team: {currentTeam.Name} for league: {currentTeam.League.Name}");
                return new ResultModel { Id = team.Id, Message = "League is locked", Success = false };
            }

            // remove existing assigned players
            var currentPlayers = await _db.FantasyTeamPlayers.Where(x => x.FantasyTeamId == team.Id).ToListAsync();
            _db.FantasyTeamPlayers.RemoveRange(currentPlayers);
            await _db.SaveChangesAsync();

            // add the new ones in
            foreach (var player in team.FantasyTeamPlayers)
            {
                var playerInfo = new FantasyTeamPlayers
                {
                    FantasyTeamId = team.Id,
                    PlayerId = player.PlayerId,
                    LeagueId = currentTeam.LeagueId,
                    SeasonId = currentTeam.League.SeasonId
                };
                _db.FantasyTeamPlayers.Add(playerInfo);
            }
            currentTeam.IsValid = isValid;
             _db.FantasyTeams.Update(currentTeam);

            await _db.SaveChangesAsync();
            _logger.LogInformation($"Updated fantasy team {team.Name} for league {currentTeam.League.Name}");
            return new ResultModel { Id = team.Id, Message = "Success", Success = true };
        }

        public async Task<List<string>> Validate(FantasyTeam team)
        {
            var errors = new List<string>();

            var currentTeam = await _db.FantasyTeams.Include(x => x.League).ThenInclude(x => x.NumberPerPositions).ThenInclude(x => x.PositionType).FirstOrDefaultAsync(x => x.Id == team.Id);
            var players = _db.PlayerSeasons.Where(x => x.SeasonId == currentTeam.League.SeasonId).Include(x => x.Player);

            if (currentTeam.League.IsLocked)
            {
                errors.Add("League is currenlty locked");
                return errors;
            }

            int count = team.FantasyTeamPlayers.Count();
            var teamsCount = players.Select(x => x.TeamId).Distinct().Count();

            // total count
            if (count != teamsCount * currentTeam.League.PlayersPerTeam)
            {
                errors.Add("Not enough players selected");
            }

            // position count
            var playerPerPositionDict = new Dictionary<int, int>();
            foreach (var positionInfo in currentTeam.League.NumberPerPositions)
            {
                playerPerPositionDict.Add(positionInfo.PositionTypeId, 0);
            }
            foreach (var player in team.FantasyTeamPlayers)
            {
                var info = await players.FirstOrDefaultAsync(x => x.PlayerId == player.PlayerId);
                if (info == null)
                {
                    errors.Add($"Player of id {player.PlayerId} does not exist");
                }
                else
                {
                    playerPerPositionDict[info.PositionTypeId]++;
                }
            }
            foreach (var item in currentTeam.League.NumberPerPositions)
            {
                if (playerPerPositionDict[item.PositionTypeId] != item.Count)
                {
                    errors.Add($"{playerPerPositionDict[item.PositionTypeId]} {item.PositionType.Name} selected and expecting {item.Count}");
                }
            }

            // Find duplicates
            var duplicateItems = team.FantasyTeamPlayers.GroupBy(x => x.PlayerId).Where(x => x.Count() > 1).Select(x => x.Key);
            foreach (var item in duplicateItems)
            {
                var info = await players.FirstOrDefaultAsync(x => x.PlayerId == item);
                errors.Add($"{info.Player.FullName} is selected more than once");
            }

            // Compare against existing teams
            var otherTeams = _db.FantasyTeams.Where(x => x.LeagueId == team.LeagueId && x.Id != team.Id).Include(x => x.FantasyTeamPlayers);
            foreach (var item in otherTeams)
            {
                var completeMatch = true;
                foreach (var player in team.FantasyTeamPlayers)
                {
                    if (!item.FantasyTeamPlayers.Any(x => x.PlayerId == player.PlayerId))
                    {
                        completeMatch = false;
                        break;
                    }
                }

                if (completeMatch)
                {
                    errors.Add("Someone already has a team with the exact same line up. You need to change a player or two");
                }
            }

            return errors;
        }

        public async Task<bool> EmailAlreadyExists(string email, string userExternalId)
        {
            var users = await _db.Users.ToArrayAsync();
            return users.Any(x => email.Equals(x.Email, StringComparison.CurrentCultureIgnoreCase) && userExternalId != x.ExternalId);
        }

        public async Task<ResultModel> Update(User user)
        {
            var u = await _db.Users.FirstOrDefaultAsync(x => x.ExternalId == user.ExternalId);
            if (u != null)
            {
                u.Email = user.Email;
                u.ReceiveEmails = user.ReceiveEmails;
                u.ReceiveNegativeNotifications = user.ReceiveNegativeNotifications;
                u.ReceiveNotifications = user.ReceiveNotifications;

                if (!string.IsNullOrEmpty(user.Name))
                {
                    u.Name = user.Name;
                }
                _db.Users.Update(u);
                _logger.LogInformation($"Updated user {u.Name}");
            }
            else
            {
                await _db.Users.AddAsync(user);
                u = user;
                _logger.LogInformation($"Added user {u.Name}");
            }
            await _db.SaveChangesAsync();

            return new ResultModel { Id = u.Id, Message = "Success", Success = true };
        }

        public async Task<bool> UsernameAlreadyExists(string username, string userExternalId)
        {
            var users = await _db.Users.ToArrayAsync();
            return users.Any(x => username.Equals(x.Name, StringComparison.CurrentCultureIgnoreCase) && userExternalId != x.ExternalId);
        }

        public async Task<ResultModel> Add(NotificationToken notificationToken, string userExternalId)
        {
            var u = await _db.Users.Include(x => x.NotificationTokens).FirstOrDefaultAsync(x => x.ExternalId == userExternalId);
            if (u == null)
            {
                return new ResultModel { Id = -1, Message = "Not found", Success = false };
            }

            if (u.NotificationTokens.Any(x => x.Token == notificationToken.Token))
            {
                return new ResultModel { Id = 0, Message = "Success", Success = true };
            }
            notificationToken.UserId = u.Id;
            _db.NotificationTokens.Add(notificationToken);
            await _db.SaveChangesAsync();

            return new ResultModel { Id = notificationToken.Id, Message = "Success", Success = true };
        }

        public async Task<ResultModel> Remove(NotificationToken notificationToken)
        {
            var notifications = await _db.NotificationTokens.Where(x => x.Token == notificationToken.Token).ToListAsync();
            _db.NotificationTokens.RemoveRange(notifications);
            await _db.SaveChangesAsync();
            return new ResultModel { Id = 0, Message = "Success", Success = true };
        }
    }
}
