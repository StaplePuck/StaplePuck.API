﻿using System;
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
                else
                {
                    team.ExternalId = item.ExternalId;
                    _db.Update(team);
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

        public async Task<ResultModel> Update(GameDate gameDate)
        {
            var seasonId = gameDate.GameDateSeasons.Select(x => x.Season).Select(x => x.ExternalId).FirstOrDefault();
            var seasonInfo = await _db.Seasons.FirstOrDefaultAsync(x => x.ExternalId == seasonId);
            if (seasonInfo == null)
            {
                return new ResultModel { Id = -1, Message = "Season not found", Success = false };
            }
            var sportId = seasonInfo.SportId;
            bool updated = false;

            var existingGameDate = await _db.GameDates
                .Include(x => x.GameDateSeasons)
                .Include(x => x.PlayersStatsOnDate).ThenInclude(x => x.Player)
                .Include(x => x.PlayersStatsOnDate).ThenInclude(x => x.PlayerScores)
                //.Include(x => x.TeamsStateOnDate).ThenInclude(x => x.Team)
                .FirstOrDefaultAsync(x => x.Id == gameDate.Id);
            if (existingGameDate == null)
            {
                existingGameDate = new GameDate {
                    Id = gameDate.Id
                };
                await _db.GameDates.AddAsync(existingGameDate);
            }

            var seasons = _db.Seasons.Where(x => x.ExternalId == seasonId);
            foreach (var item in seasons)
            {
                var existingSeason = existingGameDate.GameDateSeasons.FirstOrDefault(x => x.Season.Id == item.Id);
                if (existingSeason == null)
                {
                    existingSeason = new GameDateSeason
                    {
                        SeasonId = item.Id,
                        GameDateId = gameDate.Id
                    };
                    await _db.GameDateSeason.AddAsync(existingSeason);
                }
            }

            var scoringTypes = await _db.ScoringTypes.Where(x => x.SportId == sportId).ToListAsync();
            var players = _db.Seasons.Include(x => x.PlayerSeasons).ThenInclude(x => x.Player).Where(x => x.SportId == sportId).SelectMany(x => x.PlayerSeasons).Select(x => x.Player);
            foreach (var item in gameDate.PlayersStatsOnDate)
            {
                var existingPlayer = existingGameDate.PlayersStatsOnDate.FirstOrDefault(x => x.Player.ExternalId == item.Player.ExternalId);

                if (existingPlayer == null)
                {
                    var player = await players.FirstOrDefaultAsync(x => x.ExternalId == item.Player.ExternalId);
                    if (player == null)
                    {
                        Console.Out.WriteLine($"Warning: unable to find team {item.Player.ExternalId}");
                        continue;
                    }
                    existingPlayer = new PlayerStatsOnDate
                    {
                        PlayerId = player.Id,
                        GameDateId = gameDate.Id
                    };

                    foreach (var score in item.PlayerScores)
                    {
                        var type = scoringTypes.FirstOrDefault(x => score.ScoringType.Name == x.Name);
                        var ps = new PlayerScore
                        {
                            Total = score.Total,
                            AdminOverride = false,
                            ScoringTypeId = type.Id
                        };
                        existingPlayer.PlayerScores.Add(ps);
                    }
                    await _db.AddAsync(existingPlayer);
                    await _db.SaveChangesAsync();
                    updated = true;
                }
                else
                {
                    foreach (var score in item.PlayerScores)
                    {
                        var type = scoringTypes.FirstOrDefault(x => score.ScoringType.Name == x.Name);
                        var existingScore = existingPlayer.PlayerScores.SingleOrDefault(x => x.ScoringTypeId == type.Id);
                        if (existingScore == null)
                        {
                            existingScore = new PlayerScore
                            {
                                PlayerStatsOnDateId = existingPlayer.Id,
                                Total = score.Total,
                                AdminOverride = false,
                                ScoringTypeId = type.Id
                            };
                            await _db.AddAsync(existingScore);
                            await _db.SaveChangesAsync();
                            updated = true;
                        }
                        else
                        {
                            if (existingScore.Total != score.Total && !existingScore.AdminOverride)
                            {
                                existingScore.Total = score.Total;
                                _db.Update(existingScore);
                                updated = true;
                            }
                        }
                    }

                    var zeroOut = existingPlayer.PlayerScores.Where(l1 => !item.PlayerScores
                        .Any(newScores => newScores.ScoringType.Name == l1.ScoringType.Name));
                    foreach (var zero in zeroOut)
                    {
                        if (!zero.AdminOverride)
                        {
                            zero.Total = 0;
                            _db.Update(zero);
                            updated = true;
                        }
                    }
                }
            }

            await _db.SaveChangesAsync();
            if (updated)
            {
                //foreach (var item in seasons)
                {
                    //this?.SeasonUpdated(this, new SeasonUpdatedArgs(item.SeasonId));
                }
            }

            return new ResultModel { Id = 0, Message = "Success", Success = true };
        }

        public async Task<ResultModel> Update(League league)
        {
            var existingLeague = await _db.Leagues
                .Include(x => x.FantasyTeams)
                .Include(x => x.PlayerCalculatedScores).ThenInclude(x => x.Scoring)
                .FirstOrDefaultAsync(x => x.Id == league.Id);

            if (league.FantasyTeams != null)
            {
                foreach (var team in league.FantasyTeams)
                {
                    var existingTeam = existingLeague.FantasyTeams.FirstOrDefault(x => x.Id == team.Id);
                    existingTeam.Rank = team.Rank;
                    existingTeam.Score = team.Score;
                    existingTeam.TodaysScore = team.TodaysScore;
                    _db.Update(existingTeam);
                }
            }

            if (league.PlayerCalculatedScores != null)
            {
                foreach (var item in league.PlayerCalculatedScores)
                {
                    var existingScore = existingLeague.PlayerCalculatedScores.FirstOrDefault(x => x.PlayerId == item.PlayerId);

                    if (existingScore == null)
                    {
                        existingScore = new Core.Scoring.PlayerCalculatedScore
                        {
                            LeagueId = league.Id,
                            PlayerId = item.PlayerId
                        };
                        _db.Add(existingScore);
                        await _db.SaveChangesAsync();
                    }

                    if (item.Score != 0)
                    {
                        existingScore.Score = item.Score;
                    }
                    existingScore.TodaysScore = item.TodaysScore;
                    if (item.Scoring != null)
                    {
                        foreach (var scoreItem in item.Scoring)
                        {
                            var existingItem = existingScore.Scoring.FirstOrDefault(x => x.ScoringTypeId == scoreItem.ScoringTypeId);
                            if (existingItem == null)
                            {
                                existingItem = new Core.Scoring.CalculatedScoreItem
                                {
                                    LeagueId = league.Id,
                                    PlayerId = item.PlayerId,
                                    Score = scoreItem.Score,
                                    ScoringTypeId = scoreItem.ScoringTypeId,
                                    TodaysScore = scoreItem.TodaysScore,
                                    TodaysTotal = scoreItem.TodaysTotal,
                                    Total = scoreItem.Total
                                };
                                _db.Add(existingItem);
                                await _db.SaveChangesAsync();
                            }
                            else
                            {
                                existingItem.Total = scoreItem.Total;
                                existingItem.TodaysTotal = scoreItem.TodaysTotal;
                                existingItem.TodaysScore = scoreItem.TodaysScore;
                                existingItem.Score = scoreItem.Score;
                                _db.Update(existingItem);
                            }
                        }
                    }
                }
            }

            await _db.SaveChangesAsync();
            return new ResultModel { Id = league.Id, Message = "Success", Success = true };
        }

        public async Task<ResultModel> Update(PlayerStatsOnDate playerStatsOnDate)
        {
            var existingPlayerStat = await _db.PlayerStatsOnDates.Include(x => x.PlayerScores)
                .FirstOrDefaultAsync(x => x.GameDateId == playerStatsOnDate.GameDateId && x.PlayerId == playerStatsOnDate.PlayerId);
            if (existingPlayerStat == null)
            {
                existingPlayerStat = new PlayerStatsOnDate
                {
                    GameDateId = playerStatsOnDate.GameDateId,
                    PlayerId = playerStatsOnDate.PlayerId
                };
                await _db.AddAsync(existingPlayerStat);
                await _db.SaveChangesAsync();
            }

            foreach (var item in playerStatsOnDate.PlayerScores)
            {
                var existingScore = existingPlayerStat.PlayerScores.FirstOrDefault(x => x.ScoringTypeId == item.ScoringTypeId);
                if (existingScore == null)
                {
                    existingScore = new PlayerScore
                    {
                        AdminOverride = true,
                        Total = item.Total,
                        ScoringTypeId = item.ScoringTypeId,
                        PlayerStatsOnDateId = existingPlayerStat.Id
                    };
                    await _db.AddAsync(existingScore);
                }
                else
                {
                    existingScore.Total = item.Total;
                    existingScore.AdminOverride = true;
                    _db.Update(existingScore);
                }
            }
            await _db.SaveChangesAsync();
            return new ResultModel { Id = existingPlayerStat.Id, Message = "Sucess", Success = true };
        }

        public async Task<ResultModel> Update(TeamStateForSeason[] teamStates)
        {
            if (teamStates.Length == 0)
            {
                return new ResultModel { Id = 0, Message = "No team states", Success = false };
            }

            var seasons = _db.Seasons.Where(x => x.ExternalId == teamStates.First().Season.ExternalId);
            foreach (var season in seasons)
            {
                var teams = _db.Seasons
                    .Include(x => x.TeamSeasons).ThenInclude(x => x.Team)
                    .Where(x => x.Id == season.Id)
                    .SelectMany(x => x.TeamSeasons).Select(x => x.Team);
                var existingTeamStates = _db.TeamStateForSeason.Where(x => x.SeasonId == season.Id).Include(x => x.Team);
                foreach (var item in teamStates)
                {
                    var team = await teams.FirstOrDefaultAsync(x => x.ExternalId == item.Team.ExternalId);
                    if (team == null)
                    {
                        Console.Out.WriteLine($"Team not part of season {item.Team.ExternalId}");
                        continue;
                    }
                    var existingState = await existingTeamStates.FirstOrDefaultAsync(x => x.TeamId == team.Id);
                    if (existingState == null)
                    {
                        existingState = new TeamStateForSeason
                        {
                            GameState = item.GameState,
                            TeamId = team.Id,
                            SeasonId = season.Id
                        };
                        await _db.AddAsync(existingState);
                    }
                    else
                    {
                        existingState.GameState = item.GameState;
                        _db.Update(existingState);
                    }
                }
            }

            await _db.SaveChangesAsync();
            return new ResultModel { Id = 1, Message = "Success", Success = true };
        }

        /*
            var addTasks = new List<Task>();
            var updateTasks = new List<Task>();
            foreach (var item in dateData)
            {
                var existing = dateInfo.HockeyPlayersStatsOnDate.SingleOrDefault(x => x.HockeyPlayerId == item.HockeyPlayerId);
                if (existing == null)
                {
                    addTasks.Add(context.AddAsync(item));
                    updated = true;
                }
                else
                {
                    item.Id = existing.Id; 
                    foreach (var newScore in item.HockeyPlayerScores)
                    {
                        var existingScore = existing.HockeyPlayerScores.SingleOrDefault(x => x.ScoringTypeId == newScore.ScoringTypeId);
                        if (existingScore == null)
                        {
                            newScore.HockeyPlayerStatsOnDateId = existing.Id;
                            addTasks.Add(context.AddAsync(newScore));
                            updated = true;
                        }
                        else
                        {
                            newScore.Id = existingScore.Id;
                            newScore.HockeyPlayerStatsOnDateId = existingScore.HockeyPlayerStatsOnDateId;
                            if (existingScore.Total != newScore.Total && !existingScore.AdminOverride)
                            {
                                existingScore.Total = newScore.Total;
                                context.Update(existingScore);
                                updated = true;
                            }
                        }
                    }

                    var zeroOut = existing.HockeyPlayerScores.Where(l1 => !item.HockeyPlayerScores
                        .Any(newScores => newScores.HockeyPlayerStatsOnDateId == l1.HockeyPlayerStatsOnDateId && newScores.ScoringTypeId == l1.ScoringTypeId));
                    foreach (var zero in zeroOut)
                    {
                        if (!zero.AdminOverride)
                        {
                            zero.Total = 0;
                            context.Update(zero);
                            updated = true;
                        }
                    }
                }
            }

            // For someone that lost a point
            foreach (var item in dateInfo.HockeyPlayersStatsOnDate.Where(l1 => !dateData.Any(newData => newData.HockeyPlayerId == l1.HockeyPlayerId && newData.GameDateId == l1.GameDateId)))
            {
                foreach (var score in item.HockeyPlayerScores)
                {
                    score.Total = 0;
                }
                context.Update(item);
                updated = true;
            }*/
    }
}
