using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace StaplePuck.Data.Migrations
{
    public partial class ScoringUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LeagueScores",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    LeagueId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeagueScores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeagueScores_Leagues_LeagueId",
                        column: x => x.LeagueId,
                        principalTable: "Leagues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FantasyTeamScores",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    FantasyTeamId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Rank = table.Column<int>(nullable: false),
                    Score = table.Column<int>(nullable: false),
                    TodaysScore = table.Column<int>(nullable: false),
                    LeagueScoreId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FantasyTeamScores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FantasyTeamScores_FantasyTeams_FantasyTeamId",
                        column: x => x.FantasyTeamId,
                        principalTable: "FantasyTeams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FantasyTeamScores_LeagueScores_LeagueScoreId",
                        column: x => x.LeagueScoreId,
                        principalTable: "LeagueScores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PlayerCalculatedScores",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    PlayerId = table.Column<int>(nullable: false),
                    LeagueId = table.Column<int>(nullable: false),
                    Count = table.Column<int>(nullable: false),
                    GameState = table.Column<int>(nullable: false),
                    Score = table.Column<int>(nullable: false),
                    TodaysScore = table.Column<int>(nullable: false),
                    LeagueScoreId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerCalculatedScores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerCalculatedScores_Leagues_LeagueId",
                        column: x => x.LeagueId,
                        principalTable: "Leagues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerCalculatedScores_LeagueScores_LeagueScoreId",
                        column: x => x.LeagueScoreId,
                        principalTable: "LeagueScores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlayerCalculatedScores_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CalculatedScoreItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    PlayerCalculatedScoreId = table.Column<int>(nullable: false),
                    ScoringTypeId = table.Column<int>(nullable: false),
                    Total = table.Column<int>(nullable: false),
                    TodaysTotal = table.Column<int>(nullable: false),
                    Score = table.Column<int>(nullable: false),
                    TodaysScore = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalculatedScoreItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CalculatedScoreItems_PlayerCalculatedScores_PlayerCalculate~",
                        column: x => x.PlayerCalculatedScoreId,
                        principalTable: "PlayerCalculatedScores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CalculatedScoreItems_ScoringTypes_ScoringTypeId",
                        column: x => x.ScoringTypeId,
                        principalTable: "ScoringTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CalculatedScoreItems_PlayerCalculatedScoreId",
                table: "CalculatedScoreItems",
                column: "PlayerCalculatedScoreId");

            migrationBuilder.CreateIndex(
                name: "IX_CalculatedScoreItems_ScoringTypeId",
                table: "CalculatedScoreItems",
                column: "ScoringTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_FantasyTeamScores_FantasyTeamId",
                table: "FantasyTeamScores",
                column: "FantasyTeamId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FantasyTeamScores_LeagueScoreId",
                table: "FantasyTeamScores",
                column: "LeagueScoreId");

            migrationBuilder.CreateIndex(
                name: "IX_LeagueScores_LeagueId",
                table: "LeagueScores",
                column: "LeagueId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerCalculatedScores_LeagueId",
                table: "PlayerCalculatedScores",
                column: "LeagueId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerCalculatedScores_LeagueScoreId",
                table: "PlayerCalculatedScores",
                column: "LeagueScoreId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerCalculatedScores_PlayerId",
                table: "PlayerCalculatedScores",
                column: "PlayerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CalculatedScoreItems");

            migrationBuilder.DropTable(
                name: "FantasyTeamScores");

            migrationBuilder.DropTable(
                name: "PlayerCalculatedScores");

            migrationBuilder.DropTable(
                name: "LeagueScores");
        }
    }
}
