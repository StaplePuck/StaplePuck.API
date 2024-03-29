﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StaplePuck.Core.Fantasy;
using StaplePuck.Core.Stats;
using Microsoft.EntityFrameworkCore;

namespace StaplePuck.Data
{
    public static class StaplePuckSeedData
    {
        public static void EnsureSeedData(this StaplePuckContext db)
        {
            var t = db.FantasyTeamPlayers.Include(x => x.PlayerCalculatedScore).ToList();
            if (!db.Sports.Any())
            {
                var sports = new List<Sport>
                {
                    new Sport{ Name = "Hockey" }
                };
                db.Sports.AddRange(sports);
                db.SaveChanges();
            }

            if (!db.ScoringTypes.Any())
            { 
                var sport = db.Sports.FirstOrDefault(x => x.Name == "Hockey");
                if (sport != null)
                {
                    var types = new List<ScoringType>
                    {
                        new ScoringType{ Name = "Goal", ShortName = "G", SportId = sport.Id },
                        new ScoringType { Name = "Assist", ShortName = "A", SportId = sport.Id },
                        new ScoringType { Name = "Win", ShortName = "W", SportId = sport.Id },
                        new ScoringType { Name = "Shutout", ShortName = "SO", SportId = sport.Id },
                        new ScoringType { Name = "Shots", ShortName = "S", SportId = sport.Id },
                        new ScoringType { Name = "Shorthanded Goal", ShortName = "SHG", SportId = sport.Id },
                        new ScoringType { Name = "Game Winning Goal", ShortName = "GWG", SportId = sport.Id },
                        new ScoringType { Name = "Series Clinching Goal", ShortName = "SCG", SportId = sport.Id },
                        new ScoringType { Name = "Overtime Goal", ShortName = "OTG", SportId = sport.Id }
                    };
                    db.ScoringTypes.AddRange(types);
                    db.SaveChanges();
                }
            }

            if (!db.Positions.Any())
            {
                var sportId = db.Sports.First(x => x.Name == "Hockey").Id;
                var positions = new List<PositionType>
                {
                    new PositionType { Name = "Undefined", ShortName = "U", SportId = sportId },
                    new PositionType { Name = "Forward", ShortName = "F", SportId = sportId },
                    new PositionType { Name = "Defenseman", ShortName = "D", SportId = sportId },
                    new PositionType { Name = "Goalie", ShortName = "G", SportId = sportId }
                };
                db.Positions.AddRange(positions);
                db.SaveChanges();
            }

            if (!db.ScoringPositions.Any())
            {
                var sportId = db.Sports.First(x => x.Name == "Hockey").Id;
                var unknownId = db.Positions.First(x => x.ShortName == "U" && x.SportId == sportId).Id;
                var forwardId = db.Positions.First(x => x.ShortName == "F" && x.SportId == sportId).Id;
                var defendseId = db.Positions.First(x => x.ShortName == "D" && x.SportId == sportId).Id;
                var goalieId = db.Positions.First(x => x.ShortName == "G" && x.SportId == sportId).Id;

                var goal = db.ScoringTypes.First(x => x.ShortName == "G" && x.SportId == sportId).Id;
                var assist = db.ScoringTypes.First(x => x.ShortName == "A" && x.SportId == sportId).Id;
                var win = db.ScoringTypes.First(x => x.ShortName == "W" && x.SportId == sportId).Id;
                var so = db.ScoringTypes.First(x => x.ShortName == "SO" && x.SportId == sportId).Id;
                var shot = db.ScoringTypes.First(x => x.ShortName == "S" && x.SportId == sportId).Id;
                var shg = db.ScoringTypes.First(x => x.ShortName == "SHG" && x.SportId == sportId).Id;
                var gwg = db.ScoringTypes.First(x => x.ShortName == "GWG" && x.SportId == sportId).Id;
                var scg = db.ScoringTypes.First(x => x.ShortName == "SCG" && x.SportId == sportId).Id;
                var otg = db.ScoringTypes.First(x => x.ShortName == "OTG" && x.SportId == sportId).Id;
                var scoringPositions = new List<ScoringPositions>
                {
                    new ScoringPositions { ScoringTypeId = goal, PositionTypeId = forwardId },
                    new ScoringPositions { ScoringTypeId = goal, PositionTypeId = defendseId },
                    new ScoringPositions { ScoringTypeId = goal, PositionTypeId = goalieId },

                    new ScoringPositions { ScoringTypeId = assist, PositionTypeId = forwardId },
                    new ScoringPositions { ScoringTypeId = assist, PositionTypeId = defendseId },
                    new ScoringPositions { ScoringTypeId = assist, PositionTypeId = goalieId },

                    new ScoringPositions { ScoringTypeId = win, PositionTypeId = goalieId },
                    new ScoringPositions { ScoringTypeId = so, PositionTypeId = goalieId },
                    new ScoringPositions { ScoringTypeId = shot, PositionTypeId = goalieId },

                    new ScoringPositions { ScoringTypeId = shg, PositionTypeId = unknownId },
                    new ScoringPositions { ScoringTypeId = gwg, PositionTypeId = unknownId },
                    new ScoringPositions { ScoringTypeId = scg, PositionTypeId = unknownId },
                    new ScoringPositions { ScoringTypeId = otg, PositionTypeId = unknownId }
                };

                db.ScoringPositions.AddRange(scoringPositions);
                db.SaveChanges();
            }

            //var league2 = db.Leagues.Include(x => x.FantasyTeams).ThenInclude(x => x.FantasyTeamPlayers).FirstOrDefault(x => x.Id == 2);
            //foreach (var team in league2.FantasyTeams)
            //{
            //    foreach (var item in team.FantasyTeamPlayers)
            //    {
            //        item.LeagueId = 2;
            //    }
            //    db.UpdateRange(team.FantasyTeamPlayers);
            //}
            //db.SaveChanges();

            //var teamPlayers = db.FantasyTeamPlayers.Include(x => x.League);
            //foreach (var player in teamPlayers)
            //{
            //    player.SeasonId = player.League.SeasonId;
            //    db.Update(player);
            //}
            //db.SaveChanges();
        }
    }
}
