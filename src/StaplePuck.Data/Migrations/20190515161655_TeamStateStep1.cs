using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace StaplePuck.Data.Migrations
{
    public partial class TeamStateStep1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TeamStateOnDate");

            migrationBuilder.AddColumn<int>(
                name: "SeasonId",
                table: "FantasyTeamPlayers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TeamStateForSeason",
                columns: table => new
                {
                    TeamId = table.Column<int>(nullable: false),
                    SeasonId = table.Column<int>(nullable: false),
                    GameState = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamStateForSeason", x => new { x.SeasonId, x.TeamId });
                    table.ForeignKey(
                        name: "FK_TeamStateForSeason_Seasons_SeasonId",
                        column: x => x.SeasonId,
                        principalTable: "Seasons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeamStateForSeason_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TeamStateForSeason_TeamId",
                table: "TeamStateForSeason",
                column: "TeamId");

            migrationBuilder.Sql("INSERT INTO public.\"TeamStateForSeason\" (\"TeamId\", \"SeasonId\", \"GameState\") VALUES (1, 1, 0)");
            migrationBuilder.Sql("UPDATE public.\"FantasyTeamPlayers\" SET \"SeasonId\" = 1 where \"SeasonId\" IS NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TeamStateForSeason");

            migrationBuilder.DropColumn(
                name: "SeasonId",
                table: "FantasyTeamPlayers");

            migrationBuilder.CreateTable(
                name: "TeamStateOnDate",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    GameDateId = table.Column<string>(nullable: true),
                    GameState = table.Column<int>(nullable: false),
                    TeamId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamStateOnDate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeamStateOnDate_GameDates_GameDateId",
                        column: x => x.GameDateId,
                        principalTable: "GameDates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TeamStateOnDate_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TeamStateOnDate_GameDateId",
                table: "TeamStateOnDate",
                column: "GameDateId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamStateOnDate_TeamId",
                table: "TeamStateOnDate",
                column: "TeamId");
        }
    }
}
