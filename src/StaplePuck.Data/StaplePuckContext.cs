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
        public DbSet<ScoringType> ScoringTypes { get; set; }
        public DbSet<Season> Seasons { get; set; }
        public DbSet<Sport> Sports { get; set; }
        public DbSet<Team> Teams { get; set; }
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

            // HockeyPlayerSeason
            modelBuilder.Entity<PlayerSeason>()
                .HasKey(h => new { h.PlayerId, h.SeasonId });

            modelBuilder.Entity<PlayerSeason>()
                .HasOne(hp => hp.Season)
                .WithMany(x => x.HockeyPlayerSeasons)
                .HasForeignKey(x => x.SeasonId);

            modelBuilder.Entity<PlayerSeason>()
                .HasOne(p => p.Player)
                .WithMany(x => x.PlayerSeasons)
                .HasForeignKey(x => x.PlayerId);

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
        }
    }
}
