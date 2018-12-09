using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace StaplePuck.Data.Migrations
{
    public partial class InitialDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GameDates",
                columns: table => new
                {
                    GameDateId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameDates", x => x.GameDateId);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    PlayerId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    FullName = table.Column<string>(nullable: true),
                    ExternalId = table.Column<int>(nullable: false),
                    ShortName = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Position = table.Column<int>(nullable: false),
                    Number = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.PlayerId);
                });

            migrationBuilder.CreateTable(
                name: "Sports",
                columns: table => new
                {
                    SportId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sports", x => x.SportId);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    TeamId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: true),
                    ShortName = table.Column<string>(nullable: true),
                    ExternalId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.TeamId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "PlayerStatsOnDates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    PlayerId = table.Column<int>(nullable: false),
                    GameDateId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerStatsOnDates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerStatsOnDates_GameDates_GameDateId",
                        column: x => x.GameDateId,
                        principalTable: "GameDates",
                        principalColumn: "GameDateId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlayerStatsOnDates_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScoringTypes",
                columns: table => new
                {
                    ScoringTypeId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: true),
                    ShortName = table.Column<string>(nullable: true),
                    DeclaredPositions = table.Column<int>(nullable: false),
                    SportId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScoringTypes", x => x.ScoringTypeId);
                    table.ForeignKey(
                        name: "FK_ScoringTypes_Sports_SportId",
                        column: x => x.SportId,
                        principalTable: "Sports",
                        principalColumn: "SportId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Seasons",
                columns: table => new
                {
                    SeasonId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    FullName = table.Column<string>(nullable: true),
                    IsPlayoffs = table.Column<bool>(nullable: false),
                    Provider = table.Column<string>(nullable: true),
                    ExternalId = table.Column<string>(nullable: true),
                    StartRound = table.Column<int>(nullable: false),
                    SportId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seasons", x => x.SeasonId);
                    table.ForeignKey(
                        name: "FK_Seasons_Sports_SportId",
                        column: x => x.SportId,
                        principalTable: "Sports",
                        principalColumn: "SportId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeamStateOnDate",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    TeamId = table.Column<int>(nullable: false),
                    GameDateId = table.Column<string>(nullable: true),
                    GameState = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamStateOnDate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeamStateOnDate_GameDates_GameDateId",
                        column: x => x.GameDateId,
                        principalTable: "GameDates",
                        principalColumn: "GameDateId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TeamStateOnDate_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlayerScores",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    PlayerStatsOnDateId = table.Column<int>(nullable: false),
                    ScoringTypeId = table.Column<int>(nullable: false),
                    Total = table.Column<int>(nullable: false),
                    AdminOverride = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerScores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerScores_PlayerStatsOnDates_PlayerStatsOnDateId",
                        column: x => x.PlayerStatsOnDateId,
                        principalTable: "PlayerStatsOnDates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerScores_ScoringTypes_ScoringTypeId",
                        column: x => x.ScoringTypeId,
                        principalTable: "ScoringTypes",
                        principalColumn: "ScoringTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GameDateSeason",
                columns: table => new
                {
                    GameDateId = table.Column<string>(nullable: false),
                    SeasonId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameDateSeason", x => new { x.GameDateId, x.SeasonId });
                    table.ForeignKey(
                        name: "FK_GameDateSeason_GameDates_GameDateId",
                        column: x => x.GameDateId,
                        principalTable: "GameDates",
                        principalColumn: "GameDateId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameDateSeason_Seasons_SeasonId",
                        column: x => x.SeasonId,
                        principalTable: "Seasons",
                        principalColumn: "SeasonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Leagues",
                columns: table => new
                {
                    LeagueId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: true),
                    SeasonId = table.Column<int>(nullable: false),
                    CommissionerUserId = table.Column<int>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Announcement = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    IsLocked = table.Column<bool>(nullable: false),
                    PaymentInfo = table.Column<string>(nullable: true),
                    AllowMultipleTeams = table.Column<bool>(nullable: false),
                    PlayersPerTeam = table.Column<int>(nullable: false),
                    SportId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leagues", x => x.LeagueId);
                    table.ForeignKey(
                        name: "FK_Leagues_Users_CommissionerUserId",
                        column: x => x.CommissionerUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Leagues_Seasons_SeasonId",
                        column: x => x.SeasonId,
                        principalTable: "Seasons",
                        principalColumn: "SeasonId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Leagues_Sports_SportId",
                        column: x => x.SportId,
                        principalTable: "Sports",
                        principalColumn: "SportId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlayerSeasons",
                columns: table => new
                {
                    PlayerId = table.Column<int>(nullable: false),
                    SeasonId = table.Column<int>(nullable: false),
                    TeamId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerSeasons", x => new { x.PlayerId, x.SeasonId });
                    table.ForeignKey(
                        name: "FK_PlayerSeasons_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerSeasons_Seasons_SeasonId",
                        column: x => x.SeasonId,
                        principalTable: "Seasons",
                        principalColumn: "SeasonId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerSeasons_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FantasyTeams",
                columns: table => new
                {
                    FantasyTeamId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    UserId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    LeagueId = table.Column<int>(nullable: false),
                    IsPaid = table.Column<bool>(nullable: false),
                    IsValid = table.Column<bool>(nullable: false),
                    ReceiveEmails = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FantasyTeams", x => x.FantasyTeamId);
                    table.ForeignKey(
                        name: "FK_FantasyTeams_Leagues_LeagueId",
                        column: x => x.LeagueId,
                        principalTable: "Leagues",
                        principalColumn: "LeagueId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FantasyTeams_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LeagueMails",
                columns: table => new
                {
                    LeagueMailId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    LeagueId = table.Column<int>(nullable: false),
                    GameDateId = table.Column<string>(nullable: true),
                    Message = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeagueMails", x => x.LeagueMailId);
                    table.ForeignKey(
                        name: "FK_LeagueMails_GameDates_GameDateId",
                        column: x => x.GameDateId,
                        principalTable: "GameDates",
                        principalColumn: "GameDateId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LeagueMails_Leagues_LeagueId",
                        column: x => x.LeagueId,
                        principalTable: "Leagues",
                        principalColumn: "LeagueId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NumberPerPositions",
                columns: table => new
                {
                    NumberPerPositionId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Position = table.Column<int>(nullable: false),
                    Count = table.Column<int>(nullable: false),
                    LeagueId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NumberPerPositions", x => x.NumberPerPositionId);
                    table.ForeignKey(
                        name: "FK_NumberPerPositions_Leagues_LeagueId",
                        column: x => x.LeagueId,
                        principalTable: "Leagues",
                        principalColumn: "LeagueId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScoringRulePoints",
                columns: table => new
                {
                    ScoringRulePointsId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Position = table.Column<int>(nullable: false),
                    PointsPerScore = table.Column<int>(nullable: false),
                    ScoringTypeId = table.Column<int>(nullable: false),
                    LeagueId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScoringRulePoints", x => x.ScoringRulePointsId);
                    table.ForeignKey(
                        name: "FK_ScoringRulePoints_Leagues_LeagueId",
                        column: x => x.LeagueId,
                        principalTable: "Leagues",
                        principalColumn: "LeagueId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScoringRulePoints_ScoringTypes_ScoringTypeId",
                        column: x => x.ScoringTypeId,
                        principalTable: "ScoringTypes",
                        principalColumn: "ScoringTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FantasyTeamPlayers",
                columns: table => new
                {
                    FantasyTeamId = table.Column<int>(nullable: false),
                    PlayerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FantasyTeamPlayers", x => new { x.FantasyTeamId, x.PlayerId });
                    table.ForeignKey(
                        name: "FK_FantasyTeamPlayers_FantasyTeams_FantasyTeamId",
                        column: x => x.FantasyTeamId,
                        principalTable: "FantasyTeams",
                        principalColumn: "FantasyTeamId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FantasyTeamPlayers_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FantasyTeamPlayers_PlayerId",
                table: "FantasyTeamPlayers",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_FantasyTeams_LeagueId",
                table: "FantasyTeams",
                column: "LeagueId");

            migrationBuilder.CreateIndex(
                name: "IX_FantasyTeams_UserId",
                table: "FantasyTeams",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_GameDateSeason_SeasonId",
                table: "GameDateSeason",
                column: "SeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_LeagueMails_GameDateId",
                table: "LeagueMails",
                column: "GameDateId");

            migrationBuilder.CreateIndex(
                name: "IX_LeagueMails_LeagueId",
                table: "LeagueMails",
                column: "LeagueId");

            migrationBuilder.CreateIndex(
                name: "IX_Leagues_CommissionerUserId",
                table: "Leagues",
                column: "CommissionerUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Leagues_SeasonId",
                table: "Leagues",
                column: "SeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_Leagues_SportId",
                table: "Leagues",
                column: "SportId");

            migrationBuilder.CreateIndex(
                name: "IX_NumberPerPositions_LeagueId",
                table: "NumberPerPositions",
                column: "LeagueId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerScores_PlayerStatsOnDateId",
                table: "PlayerScores",
                column: "PlayerStatsOnDateId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerScores_ScoringTypeId",
                table: "PlayerScores",
                column: "ScoringTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerSeasons_SeasonId",
                table: "PlayerSeasons",
                column: "SeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerSeasons_TeamId",
                table: "PlayerSeasons",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerStatsOnDates_GameDateId",
                table: "PlayerStatsOnDates",
                column: "GameDateId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerStatsOnDates_PlayerId",
                table: "PlayerStatsOnDates",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_ScoringRulePoints_LeagueId",
                table: "ScoringRulePoints",
                column: "LeagueId");

            migrationBuilder.CreateIndex(
                name: "IX_ScoringRulePoints_ScoringTypeId",
                table: "ScoringRulePoints",
                column: "ScoringTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ScoringTypes_SportId",
                table: "ScoringTypes",
                column: "SportId");

            migrationBuilder.CreateIndex(
                name: "IX_Seasons_SportId",
                table: "Seasons",
                column: "SportId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamStateOnDate_GameDateId",
                table: "TeamStateOnDate",
                column: "GameDateId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamStateOnDate_TeamId",
                table: "TeamStateOnDate",
                column: "TeamId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FantasyTeamPlayers");

            migrationBuilder.DropTable(
                name: "GameDateSeason");

            migrationBuilder.DropTable(
                name: "LeagueMails");

            migrationBuilder.DropTable(
                name: "NumberPerPositions");

            migrationBuilder.DropTable(
                name: "PlayerScores");

            migrationBuilder.DropTable(
                name: "PlayerSeasons");

            migrationBuilder.DropTable(
                name: "ScoringRulePoints");

            migrationBuilder.DropTable(
                name: "TeamStateOnDate");

            migrationBuilder.DropTable(
                name: "FantasyTeams");

            migrationBuilder.DropTable(
                name: "PlayerStatsOnDates");

            migrationBuilder.DropTable(
                name: "ScoringTypes");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "Leagues");

            migrationBuilder.DropTable(
                name: "GameDates");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Seasons");

            migrationBuilder.DropTable(
                name: "Sports");
        }
    }
}
