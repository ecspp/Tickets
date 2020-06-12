using Microsoft.EntityFrameworkCore.Migrations;

namespace Tickets.WebAPI.Data.Migrations
{
    public partial class Changefollowuptocorrectauthorcollumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Followups_AspNetUsers_UserId",
                table: "Followups");

            migrationBuilder.DropIndex(
                name: "IX_Followups_UserId",
                table: "Followups");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Followups");

            migrationBuilder.AddColumn<int>(
                name: "AuthorId",
                table: "Followups",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Followups_AuthorId",
                table: "Followups",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Followups_AspNetUsers_AuthorId",
                table: "Followups",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Followups_AspNetUsers_AuthorId",
                table: "Followups");

            migrationBuilder.DropIndex(
                name: "IX_Followups_AuthorId",
                table: "Followups");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Followups");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Followups",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Followups_UserId",
                table: "Followups",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Followups_AspNetUsers_UserId",
                table: "Followups",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
