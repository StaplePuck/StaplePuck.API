using System;
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
                var sportId = db.Sports.FirstOrDefault(x => x.Name == "Hockey").Id;
                var types = new List<ScoringType>
                {
                    new ScoringType{ Name = "Goal", ShortName = "G", SportId = sportId }, 
                    new ScoringType { Name = "Assist", ShortName = "A", SportId = sportId },
                    new ScoringType { Name = "Win", ShortName = "W", SportId = sportId }, 
                    new ScoringType { Name = "Shutout", ShortName = "SO", SportId = sportId },
                    new ScoringType { Name = "Shots", ShortName = "S", SportId = sportId },
                    new ScoringType { Name = "Shorthanded Goal", ShortName = "SHG", SportId = sportId }, 
                    new ScoringType { Name = "Game Winning Goal", ShortName = "GWG", SportId = sportId }, 
                    new ScoringType { Name = "Series Clinching Goal", ShortName = "SCG", SportId = sportId }, 
                    new ScoringType { Name = "Overtime Goal", ShortName = "OTG", SportId = sportId } 
                };
                db.ScoringTypes.AddRange(types);
                db.SaveChanges();
            }

            if (!db.Positions.Any())
            {
                var sportId = db.Sports.FirstOrDefault(x => x.Name == "Hockey").Id;
                var positions = new List<PositionType>
                {
                    new PositionType { Name = "Undefined", ShortName = "U", SportId = sportId },
                    new PositionType { Name = "Forward", ShortName = "F", SportId = sportId },
                    new PositionType { Name = "Defendse", ShortName = "D", SportId = sportId },
                    new PositionType { Name = "Goalie", ShortName = "G", SportId = sportId }
                };
                db.Positions.AddRange(positions);
                db.SaveChanges();
            }

            if (!db.ScoringPositions.Any())
            {
                var sportId = db.Sports.FirstOrDefault(x => x.Name == "Hockey").Id;
                var unknownId = db.Positions.FirstOrDefault(x => x.ShortName == "U" && x.SportId == sportId).Id;
                var forwardId = db.Positions.FirstOrDefault(x => x.ShortName == "F" && x.SportId == sportId).Id;
                var defendseId = db.Positions.FirstOrDefault(x => x.ShortName == "D" && x.SportId == sportId).Id;
                var goalieId = db.Positions.FirstOrDefault(x => x.ShortName == "G" && x.SportId == sportId).Id;

                var goal = db.ScoringTypes.FirstOrDefault(x => x.ShortName == "G" && x.SportId == sportId).Id;
                var assist = db.ScoringTypes.FirstOrDefault(x => x.ShortName == "A" && x.SportId == sportId).Id;
                var win = db.ScoringTypes.FirstOrDefault(x => x.ShortName == "W" && x.SportId == sportId).Id;
                var so = db.ScoringTypes.FirstOrDefault(x => x.ShortName == "SO" && x.SportId == sportId).Id;
                var shot = db.ScoringTypes.FirstOrDefault(x => x.ShortName == "S" && x.SportId == sportId).Id;
                var shg = db.ScoringTypes.FirstOrDefault(x => x.ShortName == "SHG" && x.SportId == sportId).Id;
                var gwg = db.ScoringTypes.FirstOrDefault(x => x.ShortName == "GWG" && x.SportId == sportId).Id;
                var scg = db.ScoringTypes.FirstOrDefault(x => x.ShortName == "SCG" && x.SportId == sportId).Id;
                var otg = db.ScoringTypes.FirstOrDefault(x => x.ShortName == "OTG" && x.SportId == sportId).Id;
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
        }
    }
}
