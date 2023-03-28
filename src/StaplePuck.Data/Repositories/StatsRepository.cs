using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StaplePuck.Core;
using StaplePuck.Core.Fantasy;
using StaplePuck.Core.Stats;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography.X509Certificates;
using StaplePuck.Core.Models;

namespace StaplePuck.Data.Repositories
{
    public class StatsRepository : IStatsRepository
    {
        private readonly IMessageEmitter _messageEmitter;
        private readonly ILogger _logger;

        public StatsRepository(IMessageEmitter messageEmitter, ILogger<IStatsRepository> logger)
        {
            _messageEmitter = messageEmitter;
            _logger = logger;
        }

        
        public async Task<ResultModel> Add(StaplePuckContext context, Season season)
        {
            // find sport
            var sport = await context.Sports.FirstOrDefaultAsync(x => season.Sport != null && x.Name == season.Sport.Name);
            if (sport == null)
            {
                _logger.LogError($"Failed to add season, because sport {season.Sport?.Name} does not exist");
                throw new Exception("Sport not found");
            }
            _logger.LogInformation($"Adding season {season.FullName}");
            var dbSeason = await context.Seasons.FirstOrDefaultAsync(x => x.FullName == season.FullName && x.SportId == sport.Id);
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
                await context.Seasons.AddAsync(dbSeason);
                await context.SaveChangesAsync();
                _logger.LogInformation($"Added Season {season.FullName}. Id: {dbSeason.Id}");
            }

            var positions = await context.Positions.Where(x => x.SportId == sport.Id).ToListAsync();
            if (season.PlayerSeasons == null)
            {
                _logger.LogError($"Failed to update player seasons for {dbSeason.FullName} due to no players being defined");
                return new ResultModel {  Id = -1, Success = false, Message = "No players defined" };
            }
            // get list of teams
            var teams = season.PlayerSeasons.Select(x => x.Team).DistinctBy(x => x!.Name);
            foreach (var item in teams)
            {
                if (item == null)
                {
                    continue;
                }
                var team = await context.Teams.FirstOrDefaultAsync(x => x.Name == item.Name);
                if (team == null)
                {
                    var temp = await context.Teams.AddAsync(item);
                    team = temp.Entity;
                    await context.SaveChangesAsync();
                }
                else
                {
                    team.ExternalId = item.ExternalId;
                    context.Update(team);
                }

                var teamSeason = await context.TeamSeasons.FirstOrDefaultAsync(x => x.TeamId == team.Id && x.SeasonId == dbSeason.Id);
                if (teamSeason == null)
                {
                    await context.TeamSeasons.AddAsync(new TeamSeason { SeasonId = dbSeason.Id, TeamId = team.Id });
                }

                var teamStateForSeason = await context.TeamStateForSeason.FirstOrDefaultAsync(x => x.TeamId == team.Id && x.SeasonId == dbSeason.Id);
                if (teamStateForSeason == null)
                {
                    await context.TeamStateForSeason.AddAsync(new TeamStateForSeason { SeasonId = dbSeason.Id, TeamId = team.Id, GameState = Convert.ToInt32(GameState.Active) });
                }

                var players = season.PlayerSeasons.Where(x => x.Team?.Name == item.Name).Select(x => x.Player);
                foreach (var p in players)
                {
                    if (p == null)
                    {
                        continue;
                    }
                    var player = await context.Players.FirstOrDefaultAsync(x => x.ExternalId == p.ExternalId && x.SportId == sport.Id);

                    if (player == null)
                    {
                        player = p;
                        player.SportId = sport.Id;
                        await context.Players.AddAsync(p);
                    }
                    else
                    {
                        player.FullName = p.FullName;
                        player.Number = p.Number;
                        player.ShortName = p.ShortName;
                        player.FirstName = p.FirstName;
                        player.LastName = p.LastName;
                        player.ExternalId = p.ExternalId;
                        context.Players.Update(player);
                    }

                    var playerSeason = await context.PlayerSeasons.FirstOrDefaultAsync(x => x.PlayerId == player.Id &&  x.SeasonId == dbSeason.Id);
                    if (playerSeason == null)
                    {
                        var ps = season.PlayerSeasons.FirstOrDefault(x => x.Player?.ExternalId == p.ExternalId);
                        if (ps != null)
                        {
                            var position = positions.FirstOrDefault(x => x.Name == ps.PositionType?.Name);
                            if (position != null)
                            {
                                playerSeason = new PlayerSeason
                                {
                                    Season = dbSeason,
                                    Team = team,
                                    Player = player,
                                    PositionTypeId = position.Id
                                };
                                await context.PlayerSeasons.AddAsync(playerSeason);
                            }
                        }
                    }
                }
                // todo update
                //_logger.LogInformation($"Finished updating team {item.FullName} for season {dbSeason.FullName}");
                await context.SaveChangesAsync();
            }

            _logger.LogInformation($"Finshed updating season {dbSeason.FullName}");
            return new ResultModel { Id = dbSeason.Id, Message = "Success", Success = true };
        }

        public async Task<ResultModel> Update(StaplePuckContext context, GameDate gameDate)
        {
            var seasonData = gameDate.GameDateSeasons.Select(x => x.Season).FirstOrDefault();
            if (seasonData == null)
            {
                _logger.LogError("Failed to get a single date");
                return new ResultModel { Id = -1, Message = "Failed to get a single date", Success = false };
            }
            var seasonId = seasonData.ExternalId;
            var seasonInfo = await context.Seasons.FirstOrDefaultAsync(x => x.ExternalId == seasonId && x.IsPlayoffs == seasonData.IsPlayoffs);
            if (seasonInfo == null)
            {
                _logger.LogError($"Failed to update gameDate {gameDate.Id} since season not found");
                return new ResultModel { Id = -1, Message = "Season not found", Success = false };
            }
            var sportId = seasonInfo.SportId;
            bool updated = false;

            var existingGameDate = await context.GameDates
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
                await context.GameDates.AddAsync(existingGameDate);
                _logger.LogInformation($"Added gameDate {gameDate.Id}");
            }

            var seasons = context.Seasons
                .Include(x => x.Leagues)
                .Where(x => x.ExternalId == seasonId && x.IsPlayoffs == seasonData.IsPlayoffs);
            foreach (var item in seasons)
            {

                var existingSeason = existingGameDate?.GameDateSeasons?.FirstOrDefault(x => x.Season?.Id == item.Id && x.Season?.IsPlayoffs == item.IsPlayoffs);
                if (existingSeason == null)
                {
                    existingSeason = new GameDateSeason
                    {
                        SeasonId = item.Id,
                        GameDateId = gameDate.Id
                    };
                    await context.GameDateSeason.AddAsync(existingSeason);
                }
            }

            var scoringTypes = await context.ScoringTypes.Where(x => x.SportId == sportId).ToListAsync();
            var players = context.Seasons.Include(x => x.PlayerSeasons).ThenInclude(x => x.Player).Where(x => x.SportId == sportId).SelectMany(x => x.PlayerSeasons).Select(x => x.Player);
            foreach (var item in gameDate.PlayersStatsOnDate)
            {
                if (item.Player == null)
                {
                    continue;
                }
                var existingPlayer = existingGameDate.PlayersStatsOnDate.FirstOrDefault(x => x.Player != null && x.Player.ExternalId == item.Player.ExternalId);

                if (existingPlayer == null)
                {
                    var player = await players.FirstOrDefaultAsync(x => x != null && x.ExternalId == item.Player.ExternalId);
                    if (player == null)
                    {
                        _logger.LogWarning($"Unable to find team {item.Player.ExternalId}");
                        continue;
                    }
                    existingPlayer = new PlayerStatsOnDate
                    {
                        PlayerId = player.Id,
                        GameDateId = gameDate.Id
                    };

                    foreach (var score in item.PlayerScores)
                    {
                        var type = scoringTypes.FirstOrDefault(x => score.ScoringType?.Name == x.Name);
                        if (type != null)
                        {
                            var ps = new PlayerScore
                            {
                                Total = score.Total,
                                AdminOverride = false,
                                ScoringTypeId = type.Id
                            };
                            existingPlayer.PlayerScores.Add(ps);
                        }
                    }
                    await context.AddAsync(existingPlayer);
                    await context.SaveChangesAsync();
                    updated = true;
                }
                else
                {
                    foreach (var score in item.PlayerScores)
                    {
                        var type = scoringTypes.FirstOrDefault(x => score.ScoringType?.Name == x.Name);
                        if (type != null)
                        {
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
                                await context.AddAsync(existingScore);
                                await context.SaveChangesAsync();
                                updated = true;
                            }
                            else
                            {
                                if (existingScore.Total != score.Total && !existingScore.AdminOverride)
                                {
                                    existingScore.Total = score.Total;
                                    context.Update(existingScore);
                                    updated = true;
                                }
                            }
                        }
                    }

                    var zeroOut = existingPlayer.PlayerScores.Where(l1 => !item.PlayerScores
                        .Any(newScores => newScores.ScoringType?.Name == l1.ScoringType?.Name && l1.ScoringType?.Name != null));
                    foreach (var zero in zeroOut)
                    {
                        if (!zero.AdminOverride && zero.Total != 0)
                        {
                            zero.Total = 0;
                            context.Update(zero);
                            updated = true;
                        }
                    }
                }
            }

            var playerZeroOut = existingGameDate.PlayersStatsOnDate.Where(x => !gameDate.PlayersStatsOnDate
               .Any(y => x.Player?.ExternalId == y.Player?.ExternalId && y.Player?.ExternalId != null));
            foreach (var playerZero in playerZeroOut)
            {
                foreach (var zero in playerZero.PlayerScores)
                {
                    if (!zero.AdminOverride && zero.Total != 0)
                    {
                        zero.Total = 0;
                        context.Update(zero);
                        updated = true;
                    }
                }
            }

            await context.SaveChangesAsync();
            _logger.LogInformation($"Updated game date {gameDate.Id}");
            if (updated)
            {
                foreach (var item in seasons)
                {
                    foreach (var league in item.Leagues)
                    {
                        await _messageEmitter.StatsUpdated(new StatsUpdated { LeagueId = league.Id });
                    }
                }
            }

            return new ResultModel { Id = 0, Message = "Success", Success = true };
        }

        public async Task<ResultModel> Update(StaplePuckContext context, League league)
        {
            var existingLeague = await context.Leagues
                .Include(x => x.FantasyTeams)
                .Include(x => x.PlayerCalculatedScores).ThenInclude(x => x.Scoring)
                .FirstOrDefaultAsync(x => x.Id == league.Id);
            if (existingLeague == null)
            {
                return new ResultModel { Message = $"League not found {league.Id}", Success = false };
            }
            var teamChanges = new List<FantansyTeamChanged>();
            if (league.FantasyTeams != null)
            {
                foreach (var team in league.FantasyTeams)
                {
                    var existingTeam = existingLeague.FantasyTeams.FirstOrDefault(x => x.Id == team.Id);
                    if (existingTeam != null)
                    {
                        if (existingTeam.Score != team.Score || existingTeam.Rank != team.Rank)
                        {
                            var teamChange = new FantansyTeamChanged
                            {
                                FantasyTeamId = existingTeam.Id,
                                OldRank = existingTeam.Rank,
                                OldScore = existingTeam.Score,
                                CurrentRank = team.Rank,
                                CurrentScore = team.Score
                            };
                            teamChanges.Add(teamChange);
                        }
                        existingTeam.Rank = team.Rank;
                        existingTeam.Score = team.Score;
                        existingTeam.TodaysScore = team.TodaysScore;
                        context.Update(existingTeam);
                    }
                }
            }

            var playerScorechanges = new List<PlayerScoreUpdated>();
            if (league.PlayerCalculatedScores != null)
            {
                foreach (var item in league.PlayerCalculatedScores)
                {
                    var existingScore = existingLeague.PlayerCalculatedScores.FirstOrDefault(x => x.PlayerId == item.PlayerId);

                    var playerScoreUpdated = new PlayerScoreUpdated
                    {
                        PlayerId = item.PlayerId
                    };
                    if (existingScore == null)
                    {
                        existingScore = new Core.Scoring.PlayerCalculatedScore
                        {
                            LeagueId = league.Id,
                            PlayerId = item.PlayerId,
                            NumberOfSelectedByTeams = item.NumberOfSelectedByTeams,
                            SeasonId = existingLeague.SeasonId
                        };
                        context.Add(existingScore);
                        await context.SaveChangesAsync();
                    }
                    else
                    {
                        playerScoreUpdated.OldScore = existingScore.Score;
                        if (item.NumberOfSelectedByTeams >= 0)
                        {
                            existingScore.NumberOfSelectedByTeams = item.NumberOfSelectedByTeams;
                        }
                    }

                    if (item.Score != 0)
                    {
                        existingScore.Score = item.Score;
                        playerScoreUpdated.CurrentScore = item.Score;
                    }
                    existingScore.TodaysScore = item.TodaysScore;
                    if (item.Scoring != null)
                    {
                        var scoringList = new List<PlayerScoreTypeUpdated>();
                        foreach (var scoreItem in item.Scoring)
                        {
                            var existingItem = existingScore.Scoring.FirstOrDefault(x => x.ScoringTypeId == scoreItem.ScoringTypeId);
                            var scoreTypeUpdated = new PlayerScoreTypeUpdated
                            {
                                ScoreTypeId = scoreItem.ScoringTypeId
                            };
                            if (existingItem == null)
                            {
                                scoreTypeUpdated.OldScore = 0;
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
                                // !!!! new score
                                context.Add(existingItem);
                                await context.SaveChangesAsync();
                            }
                            else
                            {
                                scoreTypeUpdated.OldScore = existingItem.Total;
                                existingItem.Total = scoreItem.Total;
                                existingItem.TodaysTotal = scoreItem.TodaysTotal;
                                existingItem.TodaysScore = scoreItem.TodaysScore;
                                existingItem.Score = scoreItem.Score;
                                context.Update(existingItem);
                            }
                            scoreTypeUpdated.CurrentScore = scoreItem.Total;
                            if (scoreTypeUpdated.CurrentScore != scoreTypeUpdated.OldScore)
                            {
                                scoringList.Add(scoreTypeUpdated);
                            }
                        }
                        playerScoreUpdated.ScoringTypesUpdated = scoringList;

                        foreach (var existing in existingScore.Scoring)
                        {
                            if (!item.Scoring.Any(x => x.ScoringTypeId == existing.ScoringTypeId))
                            {
                                // zero out the score
                                existing.Total = 0;
                                existing.TodaysScore = 0;
                                existing.TodaysTotal = 0;
                                existing.Score = 0;
                                context.Update(existing);
                            }
                        }
                    }

                    if (playerScoreUpdated.OldScore != playerScoreUpdated.CurrentScore)
                    {
                        playerScorechanges.Add(playerScoreUpdated);
                    }
                }
            }

            await context.SaveChangesAsync();
            _logger.LogInformation($"Updated league {league.Name}");
            
            if (teamChanges.Count > 0 || playerScorechanges.Count > 0)
            {
                var updated = new ScoreUpdated
                {
                    LeagueId = league.Id,
                    FantansyTeamChanges = teamChanges,
                    PlayersScoreUpdated = playerScorechanges
                };
                await _messageEmitter.ScoreUpdated(updated);
            }

            return new ResultModel { Id = league.Id, Message = "Success", Success = true };
        }

        public async Task<ResultModel> Update(StaplePuckContext context, PlayerStatsOnDate playerStatsOnDate)
        {
            var existingPlayerStat = await context.PlayerStatsOnDates.Include(x => x.PlayerScores)
                .FirstOrDefaultAsync(x => x.GameDateId == playerStatsOnDate.GameDateId && x.PlayerId == playerStatsOnDate.PlayerId);
            if (existingPlayerStat == null)
            {
                existingPlayerStat = new PlayerStatsOnDate
                {
                    GameDateId = playerStatsOnDate.GameDateId,
                    PlayerId = playerStatsOnDate.PlayerId
                };
                await context.AddAsync(existingPlayerStat);
                await context.SaveChangesAsync();
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
                    await context.AddAsync(existingScore);
                }
                else
                {
                    existingScore.Total = item.Total;
                    existingScore.AdminOverride = true;
                    context.Update(existingScore);
                }
            }
            await context.SaveChangesAsync();
            _logger.LogInformation($"Updated player stats on date for date {playerStatsOnDate.GameDateId}");
            return new ResultModel { Id = existingPlayerStat.Id, Message = "Sucess", Success = true };
        }

        public async Task<ResultModel> Update(StaplePuckContext context, TeamStateForSeason[] teamStates)
        {
            if (teamStates.Length == 0)
            {
                return new ResultModel { Id = 0, Message = "No team states", Success = false };
            }

            var firstSeson = teamStates.First().Season;
            if (firstSeson == null)
            {
                return new ResultModel { Id = 0, Message = "No team states", Success = false };
            }
            var seasons = await context.Seasons.Where(x => teamStates.First().Season != null && x.ExternalId == firstSeson.ExternalId && x.IsPlayoffs == firstSeson.IsPlayoffs).ToListAsync();
            foreach (var season in seasons)
            {
                var teams = await context.Seasons
                    .Include(x => x.TeamSeasons).ThenInclude(x => x.Team)
                    .Where(x => x.Id == season.Id)
                    .SelectMany(x => x.TeamSeasons).Select(x => x.Team).ToListAsync();
                var existingTeamStates = await context.TeamStateForSeason.Where(x => x.SeasonId == season.Id).Include(x => x.Team).ToListAsync();
                foreach (var item in teamStates)
                {
                    var team = teams.First(x => x != null && x.ExternalId == item.Team?.ExternalId);
                    if (team == null)
                    {
                        Console.Out.WriteLine($"Team not part of season {item.Team?.ExternalId}");
                        continue;
                    }
                    var existingState = existingTeamStates.FirstOrDefault(x => x.TeamId == team.Id);
                    if (existingState == null)
                    {
                        existingState = new TeamStateForSeason
                        {
                            GameState = item.GameState,
                            TeamId = team.Id,
                            SeasonId = season.Id
                        };
                        await context.AddAsync(existingState);
                    }
                    else
                    {
                        existingState.GameState = item.GameState;
                        context.Update(existingState);
                    }
                }
            }

            await context.SaveChangesAsync();
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
