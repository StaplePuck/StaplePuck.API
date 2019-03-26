using Microsoft.EntityFrameworkCore.Migrations;

namespace StaplePuck.Data.Migrations
{
    public partial class TeamSeasonsAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PlayerSeasons_TeamId",
                table: "PlayerSeasons");

            migrationBuilder.CreateTable(
                name: "TeamSeasons",
                columns: table => new
                {
                    SeasonId = table.Column<int>(nullable: false),
                    TeamId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamSeasons", x => new { x.TeamId, x.SeasonId });
                    table.ForeignKey(
                        name: "FK_TeamSeasons_Seasons_SeasonId",
                        column: x => x.SeasonId,
                        principalTable: "Seasons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeamSeasons_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlayerSeasons_TeamId_SeasonId",
                table: "PlayerSeasons",
                columns: new[] { "TeamId", "SeasonId" });

            migrationBuilder.CreateIndex(
                name: "IX_TeamSeasons_SeasonId",
                table: "TeamSeasons",
                column: "SeasonId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerSeasons_TeamSeasons_TeamId_SeasonId",
                table: "PlayerSeasons",
                columns: new[] { "TeamId", "SeasonId" },
                principalTable: "TeamSeasons",
                principalColumns: new[] { "TeamId", "SeasonId" },
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerSeasons_TeamSeasons_TeamId_SeasonId",
                table: "PlayerSeasons");

            migrationBuilder.DropTable(
                name: "TeamSeasons");

            migrationBuilder.DropIndex(
                name: "IX_PlayerSeasons_TeamId_SeasonId",
                table: "PlayerSeasons");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerSeasons_TeamId",
                table: "PlayerSeasons",
                column: "TeamId");
        }
    }
}
