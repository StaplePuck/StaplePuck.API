﻿using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using StaplePuck.Core.Fantasy;
using StaplePuck.Core.Scoring;
using StaplePuck.Core.Stats;

namespace StaplePuck.Data
{
    public sealed class StaplePuckContext : DbContext
    {
        public static IModel StaticModel { get; } = BuildStaticModel();

        public StaplePuckContext(DbContextOptions options) : base(options)
        {
            //var result = Database.EnsureCreated();

#pragma warning disable EF1001 // Internal EF Core API usage.
            var dbOptions = options.FindExtension<Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure.Internal.NpgsqlOptionsExtension>();
#pragma warning restore EF1001 // Internal EF Core API usage.
            if (dbOptions != null && dbOptions.ConnectionString != "Fake")
            {
                //if (options.Extensions.FirstOrDefault()?.)
                //Database.Migrate();
            }
        }

        static IModel BuildStaticModel()
        {
            var builder = new DbContextOptionsBuilder();
            builder.UseNpgsql("Fake");
            using (var dbContext = new StaplePuckContext(builder.Options))
            {
                return dbContext.Model;
            }
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
        public DbSet<TeamStateForSeason> TeamStateForSeason { get; set; }

        public DbSet<CalculatedScoreItem> CalculatedScoreItems { get; set; }
        public DbSet<PlayerCalculatedScore> PlayerCalculatedScores { get; set; }
        public DbSet<NotificationToken> NotificationTokens { get; set; }

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

            modelBuilder.Entity<FantasyTeamPlayers>()
                .HasOne(ftp => ftp.PlayerCalculatedScore)
                .WithMany(p => p.FantasyTeamPlayers)
                .HasForeignKey(ftp => new { ftp.LeagueId, ftp.PlayerId });

            modelBuilder.Entity<FantasyTeamPlayers>()
                .HasOne(ftp => ftp.PlayerSeason)
                .WithMany(p => p.FantasyTeamPlayers)
                .HasForeignKey(ftp => new { ftp.PlayerId, ftp.SeasonId });

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

            modelBuilder.Entity<PlayerSeason>()
                .HasOne(p => p.TeamSeason)
                .WithMany(x => x.PlayerSeasons)
                .HasForeignKey(x => new { x.TeamId, x.SeasonId });

            modelBuilder.Entity<PlayerSeason>()
                .HasOne(p => p.TeamStateForSeason)
                .WithMany(x => x.PlayerSeasons)
                .HasForeignKey(x => new { x.SeasonId, x.TeamId });

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

            // CalculatedScoreItem>
            modelBuilder.Entity<CalculatedScoreItem>()
                .HasOne(x => x.PlayerCalculatedScore)
                .WithMany(x => x.Scoring)
                .HasForeignKey(x => new { x.LeagueId, x.PlayerId });

            // PlayerCalculatedScore
            modelBuilder.Entity<PlayerCalculatedScore>()
                .HasKey(x => new { x.LeagueId, x.PlayerId });

            modelBuilder.Entity<PlayerCalculatedScore>()
                .HasOne(x => x.League)
                .WithMany(x => x.PlayerCalculatedScores)
                .HasForeignKey(x => x.LeagueId);

            modelBuilder.Entity<PlayerCalculatedScore>()
                .HasOne(x => x.PlayerSeason)
                .WithMany(x => x.PlayerCalculatedScores)
                .HasForeignKey(x => new { x.PlayerId, x.SeasonId });

            // TeamStateForSeason
            modelBuilder.Entity<TeamStateForSeason>()
                .HasKey(x => new { x.SeasonId, x.TeamId });
        }
    }
}
