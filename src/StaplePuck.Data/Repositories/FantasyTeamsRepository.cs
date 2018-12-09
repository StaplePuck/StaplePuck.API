using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using StaplePuck.Core.Fantasy;
using StaplePuck.Core.Stats;

namespace StaplePuck.Data.Repositories
{
    public class FantasyTeamsRepository
    {
        private readonly StaplePuckContext _db;

        public FantasyTeamsRepository(StaplePuckContext db)
        {
            _db = db;
        }

        public Task<League> Get(int leagueId)
        {
            return null;
            //return await _db.Leagues.;
        }
    }
}
