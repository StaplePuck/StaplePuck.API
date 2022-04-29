using Microsoft.EntityFrameworkCore.Migrations;

namespace StaplePuck.Data.Migrations
{
    public partial class CalculatedTeamsScoresStep2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PlayerCalculatedScores_PlayerId",
                table: "PlayerCalculatedScores");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerCalculatedScores_PlayerId_SeasonId",
                table: "PlayerCalculatedScores",
                columns: new[] { "PlayerId", "SeasonId" });

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerCalculatedScores_PlayerSeasons_PlayerId_SeasonId",
                table: "PlayerCalculatedScores",
                columns: new[] { "PlayerId", "SeasonId" },
                principalTable: "PlayerSeasons",
                principalColumns: new[] { "PlayerId", "SeasonId" },
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerCalculatedScores_PlayerSeasons_PlayerId_SeasonId",
                table: "PlayerCalculatedScores");

            migrationBuilder.DropIndex(
                name: "IX_PlayerCalculatedScores_PlayerId_SeasonId",
                table: "PlayerCalculatedScores");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerCalculatedScores_PlayerId",
                table: "PlayerCalculatedScores",
                column: "PlayerId");
        }
    }
}
