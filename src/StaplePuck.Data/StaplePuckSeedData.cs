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
                var sportId = db.Sports.FirstOrDefault(x => x.Name == "Hockey").SportId;
                var types = new List<ScoringType>
                {
                    new ScoringType{ Name = "Goal", ShortName = "G", SportId = sportId, DeclaredPositions = (int)PositionType.Defense | (int)PositionType.Forward | (int)PositionType.Goalie },
                    new ScoringType{ Name = "Assist", ShortName = "A", SportId = sportId, DeclaredPositions = (int)PositionType.Defense | (int)PositionType.Forward | (int)PositionType.Goalie },
                    new ScoringType{ Name = "Win", ShortName = "W", SportId = sportId, DeclaredPositions = (int)PositionType.Goalie },
                    new ScoringType{ Name = "Shutout", ShortName = "SO", SportId = sportId, DeclaredPositions = (int)PositionType.Goalie },
                    new ScoringType{ Name = "Shorthanded Goal", ShortName = "SHG", SportId = sportId, DeclaredPositions = (int)PositionType.Undefined },
                    new ScoringType{ Name = "Game Winning Goal", ShortName = "GWG", SportId = sportId, DeclaredPositions = (int)PositionType.Undefined },
                    new ScoringType{ Name = "Series Clinching Goal", ShortName = "SCG", SportId = sportId, DeclaredPositions = (int)PositionType.Undefined },
                    new ScoringType{ Name = "Overtime Goal", ShortName = "OTG", SportId = sportId, DeclaredPositions = (int)PositionType.Undefined }
                };
                db.ScoringTypes.AddRange(types);
                db.SaveChanges();
            }
        }
    }
}
