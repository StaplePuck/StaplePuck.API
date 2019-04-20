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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LeagueId",
                table: "FantasyTeamPlayers");
        }
    }
}
