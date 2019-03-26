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
    public class StatsRepository : IStatsRepository
    {
        private readonly StaplePuckContext _db;

        public StatsRepository(StaplePuckContext db)
        {
            _db = db;
        }

        
        public async Task<ResultModel> Add(Season season)
        {
            // find sport
            var sport = await _db.Sports.FirstOrDefaultAsync(x => x.Name == season.Sport.Name);
            if (sport == null)
            {
                throw new Exception("Sport not found");
            }
            var dbSeason = await _db.Seasons.FirstOrDefaultAsync(x => x.FullName == season.FullName && x.SportId == sport.Id);
            if (dbSeason == null)
            {
                dbSeason = new Season
                {
                    ExternalId = season.ExternalId,
                    FullName = season.FullName,
                    IsPlayoffs = season.IsPlayoffs,
                    SportId = sport.Id,
                    StartRound = season.StartRound
                };
                await _db.Seasons.AddAsync(dbSeason);
                await _db.SaveChangesAsync();
            }

            var positions = await _db.Positions.Where(x => x.SportId == sport.Id).ToListAsync();
            if (season.PlayerSeasons == null)
            {
                return new ResultModel {  Id = -1, Success = false, Message = "No players defined" };
            }
            // get list of teams
            var teams = season.PlayerSeasons.Select(x => x.Team).DistinctBy(x => x.Name);
            foreach (var item in teams)
            {
                var team = await _db.Teams.FirstOrDefaultAsync(x => x.Name == item.Name);
                if (team == null)
                {
                    await _db.Teams.AddAsync(item);
                    team = item;
                }

                var teamSeason = await _db.TeamSeasons.FirstOrDefaultAsync(x => x.TeamId == team.Id && x.SeasonId == dbSeason.Id);
                if (teamSeason == null)
                {
                    await _db.TeamSeasons.AddAsync(new TeamSeason { SeasonId = dbSeason.Id, TeamId = team.Id });
                }

                var players = season.PlayerSeasons.Where(x => x.Team.Name == item.Name).Select(x => x.Player);
                foreach (var p in players)
                {
                    var player = await _db.Players.FirstOrDefaultAsync(x => x.ExternalId == p.ExternalId && x.SportId == sport.Id);

                    if (player == null)
                    {
                        player = p;
                        player.SportId = sport.Id;
                        await _db.Players.AddAsync(p);
                    }
                    else
                    {
                        player.FullName = p.FullName;
                        player.Number = p.Number;
                        player.ShortName = p.ShortName;
                        player.FirstName = p.FirstName;
                        player.LastName = p.LastName;
                        player.ExternalId = p.ExternalId;
                        _db.Players.Update(player);
                    }

                    var playerSeason = await _db.PlayerSeasons.FirstOrDefaultAsync(x => x.PlayerId == player.Id &&  x.SeasonId == dbSeason.Id);
                    if (playerSeason == null)
                    {
                        var ps = season.PlayerSeasons.FirstOrDefault(x => x.Player.ExternalId == p.ExternalId);
                        if (ps != null)
                        {
                            var position = positions.FirstOrDefault(x => x.Name == ps.PositionType.Name);
                            playerSeason = new PlayerSeason
                            {
                                Season = dbSeason,
                                Team = team,
                                Player = player,
                                PositionTypeId = position.Id
                            };
                            await _db.PlayerSeasons.AddAsync(playerSeason);
                        }
                    }
                }

                await _db.SaveChangesAsync();
            }

            return new ResultModel { Id = dbSeason.Id, Message = "Success", Success = true };
        }
    }
}
