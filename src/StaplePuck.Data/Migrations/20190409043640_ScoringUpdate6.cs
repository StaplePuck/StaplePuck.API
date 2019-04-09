using Microsoft.EntityFrameworkCore.Migrations;

namespace StaplePuck.Data.Migrations
{
    public partial class ScoringUpdate6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FantasyTeamPlayers_Leagues_LeagueId",
                table: "FantasyTeamPlayers");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_FantasyTeamPlayers_PlayerCalculatedScores_LeagueId_PlayerId",
            //    table: "FantasyTeamPlayers");

            migrationBuilder.AlterColumn<int>(
                name: "LeagueId",
                table: "FantasyTeamPlayers",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FantasyTeamPlayers_Leagues_LeagueId",
                table: "FantasyTeamPlayers",
                column: "LeagueId",
                principalTable: "Leagues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_FantasyTeamPlayers_PlayerCalculatedScores_LeagueId_PlayerId",
            //    table: "FantasyTeamPlayers",
            //    columns: new[] { "LeagueId", "PlayerId" },
            //    principalTable: "PlayerCalculatedScores",
            //    principalColumns: new[] { "LeagueId", "PlayerId" },
            //    onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FantasyTeamPlayers_Leagues_LeagueId",
                table: "FantasyTeamPlayers");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_FantasyTeamPlayers_PlayerCalculatedScores_LeagueId_PlayerId",
            //    table: "FantasyTeamPlayers");

            migrationBuilder.AlterColumn<int>(
                name: "LeagueId",
                table: "FantasyTeamPlayers",
                nullable: true,
                oldClrType: typeof(int));

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
    }
}
