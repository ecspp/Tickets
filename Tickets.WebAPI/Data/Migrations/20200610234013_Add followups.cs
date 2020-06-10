using Microsoft.EntityFrameworkCore.Migrations;

namespace Tickets.WebAPI.Data.Migrations
{
    public partial class Addfollowups : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Followup_Companies_CompanyId",
                table: "Followup");

            migrationBuilder.DropForeignKey(
                name: "FK_Followup_Tickets_TicketId",
                table: "Followup");

            migrationBuilder.DropForeignKey(
                name: "FK_Followup_AspNetUsers_UserId",
                table: "Followup");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Followup",
                table: "Followup");

            migrationBuilder.RenameTable(
                name: "Followup",
                newName: "Followups");

            migrationBuilder.RenameIndex(
                name: "IX_Followup_UserId",
                table: "Followups",
                newName: "IX_Followups_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Followup_TicketId",
                table: "Followups",
                newName: "IX_Followups_TicketId");

            migrationBuilder.RenameIndex(
                name: "IX_Followup_CompanyId",
                table: "Followups",
                newName: "IX_Followups_CompanyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Followups",
                table: "Followups",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Followups_Companies_CompanyId",
                table: "Followups",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Followups_Tickets_TicketId",
                table: "Followups",
                column: "TicketId",
                principalTable: "Tickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Followups_AspNetUsers_UserId",
                table: "Followups",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Followups_Companies_CompanyId",
                table: "Followups");

            migrationBuilder.DropForeignKey(
                name: "FK_Followups_Tickets_TicketId",
                table: "Followups");

            migrationBuilder.DropForeignKey(
                name: "FK_Followups_AspNetUsers_UserId",
                table: "Followups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Followups",
                table: "Followups");

            migrationBuilder.RenameTable(
                name: "Followups",
                newName: "Followup");

            migrationBuilder.RenameIndex(
                name: "IX_Followups_UserId",
                table: "Followup",
                newName: "IX_Followup_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Followups_TicketId",
                table: "Followup",
                newName: "IX_Followup_TicketId");

            migrationBuilder.RenameIndex(
                name: "IX_Followups_CompanyId",
                table: "Followup",
                newName: "IX_Followup_CompanyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Followup",
                table: "Followup",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Followup_Companies_CompanyId",
                table: "Followup",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Followup_Tickets_TicketId",
                table: "Followup",
                column: "TicketId",
                principalTable: "Tickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Followup_AspNetUsers_UserId",
                table: "Followup",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
