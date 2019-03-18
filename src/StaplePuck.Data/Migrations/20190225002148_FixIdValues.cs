using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace StaplePuck.Data.Migrations
{
    public partial class FixIdValues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScoringRulePoints_Positions_PositionId",
                table: "ScoringRulePoints");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ScoringRulePoints",
                table: "ScoringRulePoints");

            migrationBuilder.DropIndex(
                name: "IX_ScoringRulePoints_PositionId",
                table: "ScoringRulePoints");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ScoringRulePoints");

            migrationBuilder.DropColumn(
                name: "PositionId",
                table: "ScoringRulePoints");

            migrationBuilder.AddColumn<int>(
                name: "PositionTypeId",
                table: "ScoringRulePoints",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ScoringRulePoints",
                table: "ScoringRulePoints",
                columns: new[] { "PositionTypeId", "ScoringTypeId", "LeagueId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ScoringRulePoints_Positions_PositionTypeId",
                table: "ScoringRulePoints",
                column: "PositionTypeId",
                principalTable: "Positions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScoringRulePoints_Positions_PositionTypeId",
                table: "ScoringRulePoints");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ScoringRulePoints",
                table: "ScoringRulePoints");

            migrationBuilder.DropColumn(
                name: "PositionTypeId",
                table: "ScoringRulePoints");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ScoringRulePoints",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.AddColumn<int>(
                name: "PositionId",
                table: "ScoringRulePoints",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ScoringRulePoints",
                table: "ScoringRulePoints",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ScoringRulePoints_PositionId",
                table: "ScoringRulePoints",
                column: "PositionId");

            migrationBuilder.AddForeignKey(
                name: "FK_ScoringRulePoints_Positions_PositionId",
                table: "ScoringRulePoints",
                column: "PositionId",
                principalTable: "Positions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
