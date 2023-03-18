using Microsoft.EntityFrameworkCore.Migrations;

namespace StaplePuck.Data.Migrations
{
    public partial class ScoringWeight : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "ScoringWeight",
                table: "ScoringRulePoints",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.Sql("UPDATE public.\"ScoringRulePoints\" SET \"ScoringWeight\" = \"PointsPerScore\"");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ScoringWeight",
                table: "ScoringRulePoints");
        }
    }
}
