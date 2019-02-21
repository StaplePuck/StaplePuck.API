using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StaplePuck.Core;
using StaplePuck.Core.Data;
using StaplePuck.Core.Fantasy;
using StaplePuck.Core.Stats;
using Microsoft.EntityFrameworkCore;

namespace StaplePuck.Data.Repositories
{
    public class FantasyRepository : IFantasyRepository
    {
        private readonly StaplePuckContext _db;

        public FantasyRepository(StaplePuckContext db)
        {
            _db = db;
        }

        public async Task<ResultModel> Add(League league)
        {
            await _db.Leagues.AddAsync(league);
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
            return new ResultModel { Id = team.Id, Message = "Success", Success = true };
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
    }
}
