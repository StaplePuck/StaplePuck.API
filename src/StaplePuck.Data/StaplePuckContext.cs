using System;
using Microsoft.EntityFrameworkCore;
using StaplePuck.Core.Fantasy;
using StaplePuck.Core.Stats;

namespace StaplePuck.Data
{
    public sealed class StaplePuckContext : DbContext
    {
        public StaplePuckContext(DbContextOptions options) : base(options)
        {
            //var result = Database.EnsureCreated();
            Database.Migrate();
        }

        public DbSet<FantasyTeam> FantasyTeams { get; set; }
        public DbSet<FantasyTeamPlayers> FantasyTeamPlayers { get; set; }
        public DbSet<League> Leagues { get; set; }
        public DbSet<LeagueMail> LeagueMails { get; set; }
        public DbSet<NumberPerPosition> NumberPerPositions { get; set; }
        public DbSet<ScoringRulePoints> ScoringRulePoints { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<GameDate> GameDates { get; set; }
        public DbSet<GameDateSeason> GameDateSeason { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<PlayerScore> PlayerScores { get; set; }
        public DbSet<PlayerSeason> PlayerSeasons { get; set; }
        public DbSet<PlayerStatsOnDate> PlayerStatsOnDates { get; set; }
        public DbSet<PositionType> Positions { get; set; }
        public DbSet<ScoringPositions> ScoringPositions { get; set; }
        public DbSet<ScoringType> ScoringTypes { get; set; }
        public DbSet<Season> Seasons { get; set; }
        public DbSet<Sport> Sports { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<TeamSeason> TeamSeasons { get; set; }
        public DbSet<TeamStateOnDate> TeamStateOnDate { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Setup many to many
            modelBuilder.Entity<FantasyTeamPlayers>()
                .HasKey(f => new { f.FantasyTeamId, f.PlayerId });

            modelBuilder.Entity<FantasyTeamPlayers>()
                .HasOne(fthp => fthp.FantasyTeam)
                .WithMany(ft => ft.FantasyTeamPlayers)
                .HasForeignKey(fthp => fthp.FantasyTeamId);

            modelBuilder.Entity<FantasyTeamPlayers>()
                .HasOne(ftp => ftp.Player)
                .WithMany(p => p.FantasyTeamPlayers)
                .HasForeignKey(ftp => ftp.PlayerId);

            // PlayerSeason
            modelBuilder.Entity<PlayerSeason>()
                .HasKey(h => new { h.PlayerId, h.SeasonId });

            modelBuilder.Entity<PlayerSeason>()
                .HasOne(hp => hp.Season)
                .WithMany(x => x.PlayerSeasons)
                .HasForeignKey(x => x.SeasonId);

            modelBuilder.Entity<PlayerSeason>()
                .HasOne(p => p.Player)
                .WithMany(x => x.PlayerSeasons)
                .HasForeignKey(x => x.PlayerId);

            // TeamSeason
            modelBuilder.Entity<TeamSeason>()
                .HasKey(h => new { h.TeamId, h.SeasonId });

            modelBuilder.Entity<TeamSeason>()
                .HasOne(hp => hp.Season)
                .WithMany(x => x.TeamSeasons)
                .HasForeignKey(x => x.SeasonId);

            // GameDateSeason
            modelBuilder.Entity<GameDateSeason>()
                .HasKey(x => new { x.GameDateId, x.SeasonId });

            modelBuilder.Entity<GameDateSeason>()
                .HasOne(hp => hp.Season)
                .WithMany(x => x.GameDates)
                .HasForeignKey(x => x.GameDateId);

            modelBuilder.Entity<GameDateSeason>()
                .HasOne(hp => hp.Season)
                .WithMany(x => x.GameDates)
                .HasForeignKey(x => x.SeasonId);

            // ScoringType
            modelBuilder.Entity<ScoringType>()
                .HasOne(st => st.Sport)
                .WithMany(s => s.ScoringTypes)
                .HasForeignKey(st => st.SportId);

            // ScoringPositions
            modelBuilder.Entity<ScoringPositions>()
                .HasKey(x => new { x.PositionTypeId, x.ScoringTypeId });

            modelBuilder.Entity<ScoringPositions>()
                .HasOne(x => x.ScoringType)
                .WithMany(x => x.ScoringPositions)
                .HasForeignKey(x => x.ScoringTypeId);

            modelBuilder.Entity<ScoringPositions>()
                .HasOne(x => x.PositionType)
                .WithMany(x => x.ScoringPositions)
                .HasForeignKey(x => x.PositionTypeId);


            // ScoringRulePoints
            modelBuilder.Entity<ScoringRulePoints>()
                .HasKey(x => new { x.PositionTypeId, x.ScoringTypeId, x.LeagueId });

            modelBuilder.Entity<ScoringRulePoints>()
                .HasOne(x => x.League)
                .WithMany(x => x.ScoringRules)
                .HasForeignKey(x => x.LeagueId);

            // NumberPerPosition
            modelBuilder.Entity<NumberPerPosition>()
                .HasKey(x => new { x.PositionTypeId, x.LeagueId });

            modelBuilder.Entity<NumberPerPosition>()
                .HasOne(x => x.League)
                .WithMany(x => x.NumberPerPositions)
                .HasForeignKey(x => x.LeagueId);
        }
    }
}
