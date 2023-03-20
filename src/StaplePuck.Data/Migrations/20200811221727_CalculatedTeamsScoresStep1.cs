using Microsoft.EntityFrameworkCore.Migrations;

namespace StaplePuck.Data.Migrations
{
    public partial class CalculatedTeamsScoresStep1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SeasonId",
                table: "PlayerCalculatedScores",
                nullable: true);

            migrationBuilder.Sql("update public.\"PlayerCalculatedScores\" p SET \"SeasonId\"=l.\"SeasonId\" FROM public.\"Leagues\" l WHERE p.\"LeagueId\" = l.\"Id\";");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SeasonId",
                table: "PlayerCalculatedScores");
        }
    }
}
