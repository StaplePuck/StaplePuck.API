﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using StaplePuck.Data;

namespace StaplePuck.Data.Migrations
{
    [DbContext(typeof(StaplePuckContext))]
    [Migration("20190515203009_TeamStateStep2")]
    partial class TeamStateStep2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.0-rtm-35687")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("StaplePuck.Core.Fantasy.FantasyTeam", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsPaid");

                    b.Property<bool>("IsValid");

                    b.Property<int>("LeagueId");

                    b.Property<string>("Name");

                    b.Property<int>("Rank");

                    b.Property<int>("Score");

                    b.Property<int>("TodaysScore");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("LeagueId");

                    b.HasIndex("UserId");

                    b.ToTable("FantasyTeams");
                });

            modelBuilder.Entity("StaplePuck.Core.Fantasy.FantasyTeamPlayers", b =>
                {
                    b.Property<int>("FantasyTeamId");

                    b.Property<int>("PlayerId");

                    b.Property<int>("LeagueId");

                    b.Property<int>("SeasonId");

                    b.HasKey("FantasyTeamId", "PlayerId");

                    b.HasIndex("LeagueId", "PlayerId");

                    b.HasIndex("PlayerId", "SeasonId");

                    b.ToTable("FantasyTeamPlayers");
                });

            modelBuilder.Entity("StaplePuck.Core.Fantasy.League", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("AllowMultipleTeams");

                    b.Property<string>("Announcement");

                    b.Property<int>("CommissionerId");

                    b.Property<string>("Description");

                    b.Property<bool>("IsLocked");

                    b.Property<string>("Name");

                    b.Property<string>("Password");

                    b.Property<string>("PaymentInfo");

                    b.Property<int>("PlayersPerTeam");

                    b.Property<int>("SeasonId");

                    b.HasKey("Id");

                    b.HasIndex("CommissionerId");

                    b.HasIndex("SeasonId");

                    b.ToTable("Leagues");
                });

            modelBuilder.Entity("StaplePuck.Core.Fantasy.LeagueMail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("GameDateId");

                    b.Property<int>("LeagueId");

                    b.Property<string>("Message");

                    b.HasKey("Id");

                    b.HasIndex("GameDateId");

                    b.HasIndex("LeagueId");

                    b.ToTable("LeagueMails");
                });

            modelBuilder.Entity("StaplePuck.Core.Fantasy.NumberPerPosition", b =>
                {
                    b.Property<int>("PositionTypeId");

                    b.Property<int>("LeagueId");

                    b.Property<int>("Count");

                    b.HasKey("PositionTypeId", "LeagueId");

                    b.HasIndex("LeagueId");

                    b.ToTable("NumberPerPositions");
                });

            modelBuilder.Entity("StaplePuck.Core.Fantasy.ScoringRulePoints", b =>
                {
                    b.Property<int>("PositionTypeId");

                    b.Property<int>("ScoringTypeId");

                    b.Property<int>("LeagueId");

                    b.Property<int>("PointsPerScore");

                    b.HasKey("PositionTypeId", "ScoringTypeId", "LeagueId");

                    b.HasIndex("LeagueId");

                    b.HasIndex("ScoringTypeId");

                    b.ToTable("ScoringRulePoints");
                });

            modelBuilder.Entity("StaplePuck.Core.Fantasy.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("ExternalId");

                    b.Property<string>("Name");

                    b.Property<bool>("ReceiveEmails");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("StaplePuck.Core.Scoring.CalculatedScoreItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("LeagueId");

                    b.Property<int>("PlayerId");

                    b.Property<int>("Score");

                    b.Property<int>("ScoringTypeId");

                    b.Property<int>("TodaysScore");

                    b.Property<int>("TodaysTotal");

                    b.Property<int>("Total");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.HasIndex("ScoringTypeId");

                    b.HasIndex("LeagueId", "PlayerId");

                    b.ToTable("CalculatedScoreItems");
                });

            modelBuilder.Entity("StaplePuck.Core.Scoring.PlayerCalculatedScore", b =>
                {
                    b.Property<int>("LeagueId");

                    b.Property<int>("PlayerId");

                    b.Property<int>("GameState");

                    b.Property<int>("NumberOfSelectedByTeams");

                    b.Property<int>("Score");

                    b.Property<int>("TodaysScore");

                    b.HasKey("LeagueId", "PlayerId");

                    b.HasIndex("PlayerId");

                    b.ToTable("PlayerCalculatedScores");
                });

            modelBuilder.Entity("StaplePuck.Core.Stats.GameDate", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.HasKey("Id");

                    b.ToTable("GameDates");
                });

            modelBuilder.Entity("StaplePuck.Core.Stats.GameDateSeason", b =>
                {
                    b.Property<string>("GameDateId");

                    b.Property<int>("SeasonId");

                    b.HasKey("GameDateId", "SeasonId");

                    b.HasIndex("SeasonId");

                    b.ToTable("GameDateSeason");
                });

            modelBuilder.Entity("StaplePuck.Core.Stats.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ExternalId");

                    b.Property<string>("FirstName");

                    b.Property<string>("FullName");

                    b.Property<string>("LastName");

                    b.Property<int>("Number");

                    b.Property<string>("ShortName");

                    b.Property<int>("SportId");

                    b.HasKey("Id");

                    b.HasIndex("SportId");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("StaplePuck.Core.Stats.PlayerScore", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("AdminOverride");

                    b.Property<int>("PlayerStatsOnDateId");

                    b.Property<int>("ScoringTypeId");

                    b.Property<int>("Total");

                    b.HasKey("Id");

                    b.HasIndex("PlayerStatsOnDateId");

                    b.HasIndex("ScoringTypeId");

                    b.ToTable("PlayerScores");
                });

            modelBuilder.Entity("StaplePuck.Core.Stats.PlayerSeason", b =>
                {
                    b.Property<int>("PlayerId");

                    b.Property<int>("SeasonId");

                    b.Property<int>("PositionTypeId");

                    b.Property<int>("TeamId");

                    b.HasKey("PlayerId", "SeasonId");

                    b.HasIndex("PositionTypeId");

                    b.HasIndex("SeasonId", "TeamId");

                    b.HasIndex("TeamId", "SeasonId");

                    b.ToTable("PlayerSeasons");
                });

            modelBuilder.Entity("StaplePuck.Core.Stats.PlayerStatsOnDate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("GameDateId");

                    b.Property<int>("PlayerId");

                    b.HasKey("Id");

                    b.HasIndex("GameDateId");

                    b.HasIndex("PlayerId");

                    b.ToTable("PlayerStatsOnDates");
                });

            modelBuilder.Entity("StaplePuck.Core.Stats.PositionType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<string>("ShortName");

                    b.Property<int>("SportId");

                    b.HasKey("Id");

                    b.HasIndex("SportId");

                    b.ToTable("Positions");
                });

            modelBuilder.Entity("StaplePuck.Core.Stats.ScoringPositions", b =>
                {
                    b.Property<int>("PositionTypeId");

                    b.Property<int>("ScoringTypeId");

                    b.HasKey("PositionTypeId", "ScoringTypeId");

                    b.HasIndex("ScoringTypeId");

                    b.ToTable("ScoringPositions");
                });

            modelBuilder.Entity("StaplePuck.Core.Stats.ScoringType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<string>("ShortName");

                    b.Property<int>("SportId");

                    b.HasKey("Id");

                    b.HasIndex("SportId");

                    b.ToTable("ScoringTypes");
                });

            modelBuilder.Entity("StaplePuck.Core.Stats.Season", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ExternalId");

                    b.Property<string>("FullName");

                    b.Property<bool>("IsPlayoffs");

                    b.Property<int>("SportId");

                    b.Property<int>("StartRound");

                    b.HasKey("Id");

                    b.HasIndex("SportId");

                    b.ToTable("Seasons");
                });

            modelBuilder.Entity("StaplePuck.Core.Stats.Sport", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Sports");
                });

            modelBuilder.Entity("StaplePuck.Core.Stats.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ExternalId");

                    b.Property<string>("FullName");

                    b.Property<string>("LocationName");

                    b.Property<string>("Name");

                    b.Property<string>("ShortName");

                    b.HasKey("Id");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("StaplePuck.Core.Stats.TeamSeason", b =>
                {
                    b.Property<int>("TeamId");

                    b.Property<int>("SeasonId");

                    b.HasKey("TeamId", "SeasonId");

                    b.HasIndex("SeasonId");

                    b.ToTable("TeamSeasons");
                });

            modelBuilder.Entity("StaplePuck.Core.Stats.TeamStateForSeason", b =>
                {
                    b.Property<int>("SeasonId");

                    b.Property<int>("TeamId");

                    b.Property<int>("GameState");

                    b.HasKey("SeasonId", "TeamId");

                    b.HasIndex("TeamId");

                    b.ToTable("TeamStateForSeason");
                });

            modelBuilder.Entity("StaplePuck.Core.Fantasy.FantasyTeam", b =>
                {
                    b.HasOne("StaplePuck.Core.Fantasy.League", "League")
                        .WithMany("FantasyTeams")
                        .HasForeignKey("LeagueId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("StaplePuck.Core.Fantasy.User", "GM")
                        .WithMany("FantasyTeams")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("StaplePuck.Core.Fantasy.FantasyTeamPlayers", b =>
                {
                    b.HasOne("StaplePuck.Core.Fantasy.FantasyTeam", "FantasyTeam")
                        .WithMany("FantasyTeamPlayers")
                        .HasForeignKey("FantasyTeamId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("StaplePuck.Core.Fantasy.League", "League")
                        .WithMany()
                        .HasForeignKey("LeagueId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("StaplePuck.Core.Stats.Player", "Player")
                        .WithMany("FantasyTeamPlayers")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("StaplePuck.Core.Scoring.PlayerCalculatedScore", "PlayerCalculatedScore")
                        .WithMany()
                        .HasForeignKey("LeagueId", "PlayerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("StaplePuck.Core.Stats.PlayerSeason", "PlayerSeason")
                        .WithMany()
                        .HasForeignKey("PlayerId", "SeasonId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("StaplePuck.Core.Fantasy.League", b =>
                {
                    b.HasOne("StaplePuck.Core.Fantasy.User", "Commissioner")
                        .WithMany()
                        .HasForeignKey("CommissionerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("StaplePuck.Core.Stats.Season", "Season")
                        .WithMany("Leagues")
                        .HasForeignKey("SeasonId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("StaplePuck.Core.Fantasy.LeagueMail", b =>
                {
                    b.HasOne("StaplePuck.Core.Stats.GameDate", "GameDate")
                        .WithMany()
                        .HasForeignKey("GameDateId");

                    b.HasOne("StaplePuck.Core.Fantasy.League", "League")
                        .WithMany("LeagueMails")
                        .HasForeignKey("LeagueId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("StaplePuck.Core.Fantasy.NumberPerPosition", b =>
                {
                    b.HasOne("StaplePuck.Core.Fantasy.League", "League")
                        .WithMany("NumberPerPositions")
                        .HasForeignKey("LeagueId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("StaplePuck.Core.Stats.PositionType", "PositionType")
                        .WithMany()
                        .HasForeignKey("PositionTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("StaplePuck.Core.Fantasy.ScoringRulePoints", b =>
                {
                    b.HasOne("StaplePuck.Core.Fantasy.League", "League")
                        .WithMany("ScoringRules")
                        .HasForeignKey("LeagueId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("StaplePuck.Core.Stats.PositionType", "PositionType")
                        .WithMany()
                        .HasForeignKey("PositionTypeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("StaplePuck.Core.Stats.ScoringType", "ScoringType")
                        .WithMany()
                        .HasForeignKey("ScoringTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("StaplePuck.Core.Scoring.CalculatedScoreItem", b =>
                {
                    b.HasOne("StaplePuck.Core.Fantasy.League", "League")
                        .WithMany()
                        .HasForeignKey("LeagueId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("StaplePuck.Core.Stats.Player", "Player")
                        .WithMany()
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("StaplePuck.Core.Stats.ScoringType", "ScoringType")
                        .WithMany()
                        .HasForeignKey("ScoringTypeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("StaplePuck.Core.Scoring.PlayerCalculatedScore", "PlayerCalculatedScore")
                        .WithMany("Scoring")
                        .HasForeignKey("LeagueId", "PlayerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("StaplePuck.Core.Scoring.PlayerCalculatedScore", b =>
                {
                    b.HasOne("StaplePuck.Core.Fantasy.League", "League")
                        .WithMany("PlayerCalculatedScores")
                        .HasForeignKey("LeagueId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("StaplePuck.Core.Stats.Player", "Player")
                        .WithMany()
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("StaplePuck.Core.Stats.GameDateSeason", b =>
                {
                    b.HasOne("StaplePuck.Core.Stats.GameDate", "GameDate")
                        .WithMany("GameDateSeasons")
                        .HasForeignKey("GameDateId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("StaplePuck.Core.Stats.Season", "Season")
                        .WithMany("GameDates")
                        .HasForeignKey("SeasonId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("StaplePuck.Core.Stats.Player", b =>
                {
                    b.HasOne("StaplePuck.Core.Stats.Sport", "Sport")
                        .WithMany()
                        .HasForeignKey("SportId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("StaplePuck.Core.Stats.PlayerScore", b =>
                {
                    b.HasOne("StaplePuck.Core.Stats.PlayerStatsOnDate", "PlayerStatsOnDate")
                        .WithMany("PlayerScores")
                        .HasForeignKey("PlayerStatsOnDateId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("StaplePuck.Core.Stats.ScoringType", "ScoringType")
                        .WithMany()
                        .HasForeignKey("ScoringTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("StaplePuck.Core.Stats.PlayerSeason", b =>
                {
                    b.HasOne("StaplePuck.Core.Stats.Player", "Player")
                        .WithMany("PlayerSeasons")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("StaplePuck.Core.Stats.PositionType", "PositionType")
                        .WithMany()
                        .HasForeignKey("PositionTypeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("StaplePuck.Core.Stats.Season", "Season")
                        .WithMany("PlayerSeasons")
                        .HasForeignKey("SeasonId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("StaplePuck.Core.Stats.Team", "Team")
                        .WithMany()
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("StaplePuck.Core.Stats.TeamStateForSeason", "TeamStateForSeason")
                        .WithMany()
                        .HasForeignKey("SeasonId", "TeamId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("StaplePuck.Core.Stats.TeamSeason")
                        .WithMany("PlayerSeasons")
                        .HasForeignKey("TeamId", "SeasonId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("StaplePuck.Core.Stats.PlayerStatsOnDate", b =>
                {
                    b.HasOne("StaplePuck.Core.Stats.GameDate", "GameDate")
                        .WithMany("PlayersStatsOnDate")
                        .HasForeignKey("GameDateId");

                    b.HasOne("StaplePuck.Core.Stats.Player", "Player")
                        .WithMany("StatsOnDate")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("StaplePuck.Core.Stats.PositionType", b =>
                {
                    b.HasOne("StaplePuck.Core.Stats.Sport", "Sport")
                        .WithMany()
                        .HasForeignKey("SportId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("StaplePuck.Core.Stats.ScoringPositions", b =>
                {
                    b.HasOne("StaplePuck.Core.Stats.PositionType", "PositionType")
                        .WithMany("ScoringPositions")
                        .HasForeignKey("PositionTypeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("StaplePuck.Core.Stats.ScoringType", "ScoringType")
                        .WithMany("ScoringPositions")
                        .HasForeignKey("ScoringTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("StaplePuck.Core.Stats.ScoringType", b =>
                {
                    b.HasOne("StaplePuck.Core.Stats.Sport", "Sport")
                        .WithMany("ScoringTypes")
                        .HasForeignKey("SportId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("StaplePuck.Core.Stats.Season", b =>
                {
                    b.HasOne("StaplePuck.Core.Stats.Sport", "Sport")
                        .WithMany()
                        .HasForeignKey("SportId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("StaplePuck.Core.Stats.TeamSeason", b =>
                {
                    b.HasOne("StaplePuck.Core.Stats.Season", "Season")
                        .WithMany("TeamSeasons")
                        .HasForeignKey("SeasonId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("StaplePuck.Core.Stats.Team", "Team")
                        .WithMany()
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("StaplePuck.Core.Stats.TeamStateForSeason", b =>
                {
                    b.HasOne("StaplePuck.Core.Stats.Season", "Season")
                        .WithMany()
                        .HasForeignKey("SeasonId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("StaplePuck.Core.Stats.Team", "Team")
                        .WithMany()
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
