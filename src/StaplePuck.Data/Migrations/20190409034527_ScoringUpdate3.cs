using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace StaplePuck.Data.Migrations
{
    public partial class ScoringUpdate3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlayerCalculatedScores",
                columns: table => new
                {
                    PlayerId = table.Column<int>(nullable: false),
                    LeagueId = table.Column<int>(nullable: false),
                    NumberOfSelectedByTeams = table.Column<int>(nullable: false),
                    GameState = table.Column<int>(nullable: false),
                    Score = table.Column<int>(nullable: false),
                    TodaysScore = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerCalculatedScores", x => new { x.LeagueId, x.PlayerId });
                    table.ForeignKey(
                        name: "FK_PlayerCalculatedScores_Leagues_LeagueId",
                        column: x => x.LeagueId,
                        principalTable: "Leagues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    PlayerId = table.Column<int>(nullable: false),
                    LeagueId = table.Column<int>(nullable: false),
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
                        name: "FK_CalculatedScoreItems_Leagues_LeagueId",
                        column: x => x.LeagueId,
                        principalTable: "Leagues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CalculatedScoreItems_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CalculatedScoreItems_ScoringTypes_ScoringTypeId",
                        column: x => x.ScoringTypeId,
                        principalTable: "ScoringTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CalculatedScoreItems_PlayerCalculatedScores_LeagueId_Player~",
                        columns: x => new { x.LeagueId, x.PlayerId },
                        principalTable: "PlayerCalculatedScores",
                        principalColumns: new[] { "LeagueId", "PlayerId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FantasyTeamPlayers_LeagueId_PlayerId",
                table: "FantasyTeamPlayers",
                columns: new[] { "LeagueId", "PlayerId" });

            migrationBuilder.CreateIndex(
                name: "IX_CalculatedScoreItems_PlayerId",
                table: "CalculatedScoreItems",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_CalculatedScoreItems_ScoringTypeId",
                table: "CalculatedScoreItems",
                column: "ScoringTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CalculatedScoreItems_LeagueId_PlayerId",
                table: "CalculatedScoreItems",
                columns: new[] { "LeagueId", "PlayerId" });

            migrationBuilder.CreateIndex(
                name: "IX_PlayerCalculatedScores_PlayerId",
                table: "PlayerCalculatedScores",
                column: "PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_FantasyTeamPlayers_Leagues_LeagueId",
                table: "FantasyTeamPlayers",
                column: "LeagueId",
                principalTable: "Leagues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_FantasyTeamPlayers_PlayerCalculatedScores_LeagueId_PlayerId",
            //    table: "FantasyTeamPlayers",
            //    columns: new[] { "LeagueId", "PlayerId" },
            //    principalTable: "PlayerCalculatedScores",
            //    principalColumns: new[] { "LeagueId", "PlayerId" },
            //    onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FantasyTeamPlayers_Leagues_LeagueId",
                table: "FantasyTeamPlayers");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_FantasyTeamPlayers_PlayerCalculatedScores_LeagueId_PlayerId",
            //    table: "FantasyTeamPlayers");

            migrationBuilder.DropTable(
                name: "CalculatedScoreItems");

            migrationBuilder.DropTable(
                name: "PlayerCalculatedScores");

            migrationBuilder.DropIndex(
                name: "IX_FantasyTeamPlayers_LeagueId_PlayerId",
                table: "FantasyTeamPlayers");
        }
    }
}
