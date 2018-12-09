using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using StaplePuck.Core.Fantasy;

namespace StaplePuck.Data.Repositories
{
    public class LeaguesRepository
    {
        private readonly StaplePuckContext _db;

        public LeaguesRepository(StaplePuckContext db)
        {
            _db = db;
        }

        //public async Task<League> Get(int leagueId)
        //{
        //    return await _db.Leagues
        //        .Include(x => x.Season)
        //        .Include(x => x.Commissioner)
        //        .FirstOrDefaultAsync(x => x.LeagueId == leagueId);
        //}
    }
}
