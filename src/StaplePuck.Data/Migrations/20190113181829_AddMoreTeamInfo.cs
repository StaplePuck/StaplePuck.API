using Microsoft.EntityFrameworkCore.Migrations;

namespace StaplePuck.Data.Migrations
{
    public partial class AddMoreTeamInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Teams",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LocationName",
                table: "Teams",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "LocationName",
                table: "Teams");
        }
    }
}
