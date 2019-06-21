using Microsoft.EntityFrameworkCore.Migrations;

namespace StaplePuck.Data.Migrations
{
    public partial class TeamStateStep2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PlayerSeasons_SeasonId",
                table: "PlayerSeasons");

            migrationBuilder.DropIndex(
                name: "IX_FantasyTeamPlayers_PlayerId",
                table: "FantasyTeamPlayers");

            migrationBuilder.AlterColumn<int>(
                name: "SeasonId",
                table: "FantasyTeamPlayers",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlayerSeasons_SeasonId_TeamId",
                table: "PlayerSeasons",
                columns: new[] { "SeasonId", "TeamId" });

            migrationBuilder.CreateIndex(
                name: "IX_FantasyTeamPlayers_PlayerId_SeasonId",
                table: "FantasyTeamPlayers",
                columns: new[] { "PlayerId", "SeasonId" });

            migrationBuilder.AddForeignKey(
                name: "FK_FantasyTeamPlayers_PlayerSeasons_PlayerId_SeasonId",
                table: "FantasyTeamPlayers",
                columns: new[] { "PlayerId", "SeasonId" },
                principalTable: "PlayerSeasons",
                principalColumns: new[] { "PlayerId", "SeasonId" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerSeasons_TeamStateForSeason_SeasonId_TeamId",
                table: "PlayerSeasons",
                columns: new[] { "SeasonId", "TeamId" },
                principalTable: "TeamStateForSeason",
                principalColumns: new[] { "SeasonId", "TeamId" },
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FantasyTeamPlayers_PlayerSeasons_PlayerId_SeasonId",
                table: "FantasyTeamPlayers");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerSeasons_TeamStateForSeason_SeasonId_TeamId",
                table: "PlayerSeasons");

            migrationBuilder.DropIndex(
                name: "IX_PlayerSeasons_SeasonId_TeamId",
                table: "PlayerSeasons");

            migrationBuilder.DropIndex(
                name: "IX_FantasyTeamPlayers_PlayerId_SeasonId",
                table: "FantasyTeamPlayers");

            migrationBuilder.AlterColumn<int>(
                name: "SeasonId",
                table: "FantasyTeamPlayers",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_PlayerSeasons_SeasonId",
                table: "PlayerSeasons",
                column: "SeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_FantasyTeamPlayers_PlayerId",
                table: "FantasyTeamPlayers",
                column: "PlayerId");
        }
    }
}
