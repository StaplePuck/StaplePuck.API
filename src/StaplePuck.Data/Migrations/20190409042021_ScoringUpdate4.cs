using Microsoft.EntityFrameworkCore.Migrations;

namespace StaplePuck.Data.Migrations
{
    public partial class ScoringUpdate4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Rank",
                table: "FantasyTeams",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Score",
                table: "FantasyTeams",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TodaysScore",
                table: "FantasyTeams",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rank",
                table: "FantasyTeams");

            migrationBuilder.DropColumn(
                name: "Score",
                table: "FantasyTeams");

            migrationBuilder.DropColumn(
                name: "TodaysScore",
                table: "FantasyTeams");
        }
    }
}
