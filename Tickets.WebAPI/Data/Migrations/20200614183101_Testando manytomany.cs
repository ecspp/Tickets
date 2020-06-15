using Microsoft.EntityFrameworkCore.Migrations;

namespace Tickets.WebAPI.Data.Migrations
{
    public partial class Testandomanytomany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContactTypeContact_Contacts_ContactId",
                table: "ContactTypeContact");

            migrationBuilder.DropForeignKey(
                name: "FK_ContactTypeContact_ContactTypes_ContactTypeId",
                table: "ContactTypeContact");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContactTypeContact",
                table: "ContactTypeContact");

            migrationBuilder.RenameTable(
                name: "ContactTypeContact",
                newName: "ContactTypeContacts");

            migrationBuilder.RenameIndex(
                name: "IX_ContactTypeContact_ContactTypeId",
                table: "ContactTypeContacts",
                newName: "IX_ContactTypeContacts_ContactTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContactTypeContacts",
                table: "ContactTypeContacts",
                columns: new[] { "ContactId", "ContactTypeId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ContactTypeContacts_Contacts_ContactId",
                table: "ContactTypeContacts",
                column: "ContactId",
                principalTable: "Contacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactTypeContacts_ContactTypes_ContactTypeId",
                table: "ContactTypeContacts",
                column: "ContactTypeId",
                principalTable: "ContactTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContactTypeContacts_Contacts_ContactId",
                table: "ContactTypeContacts");

            migrationBuilder.DropForeignKey(
                name: "FK_ContactTypeContacts_ContactTypes_ContactTypeId",
                table: "ContactTypeContacts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContactTypeContacts",
                table: "ContactTypeContacts");

            migrationBuilder.RenameTable(
                name: "ContactTypeContacts",
                newName: "ContactTypeContact");

            migrationBuilder.RenameIndex(
                name: "IX_ContactTypeContacts_ContactTypeId",
                table: "ContactTypeContact",
                newName: "IX_ContactTypeContact_ContactTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContactTypeContact",
                table: "ContactTypeContact",
                columns: new[] { "ContactId", "ContactTypeId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ContactTypeContact_Contacts_ContactId",
                table: "ContactTypeContact",
                column: "ContactId",
                principalTable: "Contacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactTypeContact_ContactTypes_ContactTypeId",
                table: "ContactTypeContact",
                column: "ContactTypeId",
                principalTable: "ContactTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
