using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace StaplePuck.Data.Migrations
{
    public partial class PositionTypeUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NumberPerPositions_Positions_PositionId",
                table: "NumberPerPositions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NumberPerPositions",
                table: "NumberPerPositions");

            migrationBuilder.DropIndex(
                name: "IX_NumberPerPositions_PositionId",
                table: "NumberPerPositions");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "NumberPerPositions");

            migrationBuilder.DropColumn(
                name: "PositionId",
                table: "NumberPerPositions");

            migrationBuilder.AddColumn<int>(
                name: "PositionTypeId",
                table: "NumberPerPositions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_NumberPerPositions",
                table: "NumberPerPositions",
                columns: new[] { "PositionTypeId", "LeagueId" });

            migrationBuilder.AddForeignKey(
                name: "FK_NumberPerPositions_Positions_PositionTypeId",
                table: "NumberPerPositions",
                column: "PositionTypeId",
                principalTable: "Positions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NumberPerPositions_Positions_PositionTypeId",
                table: "NumberPerPositions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NumberPerPositions",
                table: "NumberPerPositions");

            migrationBuilder.DropColumn(
                name: "PositionTypeId",
                table: "NumberPerPositions");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "NumberPerPositions",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.AddColumn<int>(
                name: "PositionId",
                table: "NumberPerPositions",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_NumberPerPositions",
                table: "NumberPerPositions",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_NumberPerPositions_PositionId",
                table: "NumberPerPositions",
                column: "PositionId");

            migrationBuilder.AddForeignKey(
                name: "FK_NumberPerPositions_Positions_PositionId",
                table: "NumberPerPositions",
                column: "PositionId",
                principalTable: "Positions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
