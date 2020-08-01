using Microsoft.EntityFrameworkCore.Migrations;

namespace Tickets.WebAPI.Data.Migrations
{
    public partial class CpfCnpfandCorporateName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Companies");

            migrationBuilder.AddColumn<string>(
                name: "CorporateName",
                table: "Companies",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CpfCnpj",
                table: "Companies",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CorporateName",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "CpfCnpj",
                table: "Companies");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Companies",
                type: "text",
                nullable: true);
        }
    }
}
