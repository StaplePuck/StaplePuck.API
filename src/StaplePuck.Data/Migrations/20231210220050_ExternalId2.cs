using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StaplePuck.Data.Migrations
{
    /// <inheritdoc />
    public partial class ExternalId2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ExternalId2",
                table: "Teams",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExternalId2",
                table: "Players",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExternalId2",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "ExternalId2",
                table: "Players");
        }
    }
}
