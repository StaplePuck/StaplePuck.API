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

namespace StaplePuck.Data.Repositories
{
    public class FantasyRepository : IFantasyRepository
    {
        private readonly StaplePuckContext _db;
        private readonly IAuthorizationClient _authorizationClient;

        public FantasyRepository(StaplePuckContext db, IAuthorizationClient authorizationClient)
        {
            _db = db;
            _authorizationClient = authorizationClient;
        }

        public async Task<ResultModel> Add(League league)
        {
            await _db.Leagues.AddAsync(league);
            await _db.SaveChangesAsync();

            var newLeague = await _db.Leagues.Include(l => l.Commissioner).FirstOrDefaultAsync(x => x.Id == league.Id);
            var extId = newLeague.Commissioner.ExternalId;
            var groupId = await _authorizationClient.CreateGroupAsync($"League:{league.Id}");
            await _authorizationClient.AddUserToGroup(groupId, extId);

            return new ResultModel { Id = league.Id, Message = "Success", Success = true };
        }

        public async Task<ResultModel> Update(League league)
        {
            var leagueInfo = await _db.Leagues
                .Include(x => x.NumberPerPositions)
                .Include(x => x.ScoringRules)
                .FirstOrDefaultAsync(x => x.Id == league.Id);

            leagueInfo.AllowMultipleTeams = league.AllowMultipleTeams;
            leagueInfo.Announcement = league.Announcement;
            leagueInfo.Description = league.Description;
            leagueInfo.IsLocked = league.IsLocked;
            leagueInfo.Name = league.Name;
            leagueInfo.PaymentInfo = league.PaymentInfo;
            leagueInfo.PlayersPerTeam = league.PlayersPerTeam;

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

            _db.Leagues.Update(leagueInfo);
            await _db.SaveChangesAsync();
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

            var groupId = await _authorizationClient.CreateGroupAsync($"Team:{team.Id}");
            await _authorizationClient.AddUserToGroup(groupId, userExternalId);

            return new ResultModel { Id = team.Id, Message = "Success", Success = true };
        }

        public async Task<ResultModel> Update(FantasyTeam team)
        {
            var currentTeam = await _db.FantasyTeams.Include(x => x.League).ThenInclude(x => x.NumberPerPositions).FirstOrDefaultAsync(x => x.Id == team.Id);

            
            // validations....

            await Task.CompletedTask;
            return new ResultModel { Id = team.Id, Message = "Success", Success = true };
        }

        public async Task<bool> EmailAlreadyExists(string email, string userExternalId)
        {
            return await _db.Users.AnyAsync(x => email.Equals(x.Email, StringComparison.CurrentCultureIgnoreCase) && userExternalId == x.ExternalId);
        }

        public async Task<ResultModel> Update(User user)
        {
            var u = await _db.Users.FirstOrDefaultAsync(x => x.ExternalId == user.ExternalId);
            if (u != null)
            {
                u.Email = user.Email;
                u.ReceiveEmails = user.ReceiveEmails;
                u.Name = user.Name;
                _db.Users.Update(u);
            }
            else
            {
                await _db.Users.AddAsync(user);
                u = user;
            }
            await _db.SaveChangesAsync();
            return new ResultModel { Id = u.Id, Message = "Success", Success = true };
        }

        public async Task<bool> UsernameAlreadyExists(string username, string userExternalId)
        {
            return await _db.Users.AnyAsync(x => username.Equals(x.Name, StringComparison.CurrentCultureIgnoreCase) && userExternalId == x.ExternalId);
        }
    }
}
