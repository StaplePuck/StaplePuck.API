using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace StaplePuck.Data.Migrations
{
    public partial class ScoringUpdate1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LeagueId",
                table: "FantasyTeamPlayers",
                nullable: true);

            migrationBuilder.Sql("UPDATE public.\"FantasyTeamPlayers\" SET \"LeagueId\" = 1 where \"LeagueId\" IS NULL");

            migrationBuilder.CreateTable(
                name: "PlayerCalculatedScore",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    PlayerId = table.Column<int>(nullable: false),
                    LeagueId = table.Column<int>(nullable: false),
                    NumberOfSelectedByTeams = table.Column<int>(nullable: false),
                    GameState = table.Column<int>(nullable: false),
                    Score = table.Column<int>(nullable: false),
                    TodaysScore = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerCalculatedScore", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerCalculatedScore_Leagues_LeagueId",
                        column: x => x.LeagueId,
                        principalTable: "Leagues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerCalculatedScore_Players_PlayerId",
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
                    PlayerCalculatedScoreId = table.Column<int>(nullable: true),
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
                        name: "FK_CalculatedScoreItems_PlayerCalculatedScore_PlayerCalculated~",
                        column: x => x.PlayerCalculatedScoreId,
                        principalTable: "PlayerCalculatedScore",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                });

            migrationBuilder.CreateIndex(
                name: "IX_CalculatedScoreItems_LeagueId",
                table: "CalculatedScoreItems",
                column: "LeagueId");

            migrationBuilder.CreateIndex(
                name: "IX_CalculatedScoreItems_PlayerCalculatedScoreId",
                table: "CalculatedScoreItems",
                column: "PlayerCalculatedScoreId");

            migrationBuilder.CreateIndex(
                name: "IX_CalculatedScoreItems_PlayerId",
                table: "CalculatedScoreItems",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_CalculatedScoreItems_ScoringTypeId",
                table: "CalculatedScoreItems",
                column: "ScoringTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerCalculatedScore_LeagueId",
                table: "PlayerCalculatedScore",
                column: "LeagueId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerCalculatedScore_PlayerId",
                table: "PlayerCalculatedScore",
                column: "PlayerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CalculatedScoreItems");

            migrationBuilder.DropTable(
                name: "PlayerCalculatedScore");

            migrationBuilder.DropColumn(
                name: "LeagueId",
                table: "FantasyTeamPlayers");
        }
    }
}
