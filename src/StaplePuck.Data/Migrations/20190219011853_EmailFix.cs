using Microsoft.EntityFrameworkCore.Migrations;

namespace StaplePuck.Data.Migrations
{
    public partial class EmailFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Leagues_Users_CommissionerId",
                table: "Leagues");

            migrationBuilder.DropForeignKey(
                name: "FK_Leagues_Sports_SportId",
                table: "Leagues");

            migrationBuilder.DropIndex(
                name: "IX_Leagues_SportId",
                table: "Leagues");

            migrationBuilder.DropColumn(
                name: "SportId",
                table: "Leagues");

            migrationBuilder.DropColumn(
                name: "ReceiveEmails",
                table: "FantasyTeams");

            migrationBuilder.AddColumn<string>(
                name: "ExternalId",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ReceiveEmails",
                table: "Users",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<int>(
                name: "CommissionerId",
                table: "Leagues",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Leagues_Users_CommissionerId",
                table: "Leagues",
                column: "CommissionerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Leagues_Users_CommissionerId",
                table: "Leagues");

            migrationBuilder.DropColumn(
                name: "ExternalId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ReceiveEmails",
                table: "Users");

            migrationBuilder.AlterColumn<int>(
                name: "CommissionerId",
                table: "Leagues",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "SportId",
                table: "Leagues",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "ReceiveEmails",
                table: "FantasyTeams",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Leagues_SportId",
                table: "Leagues",
                column: "SportId");

            migrationBuilder.AddForeignKey(
                name: "FK_Leagues_Users_CommissionerId",
                table: "Leagues",
                column: "CommissionerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Leagues_Sports_SportId",
                table: "Leagues",
                column: "SportId",
                principalTable: "Sports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
