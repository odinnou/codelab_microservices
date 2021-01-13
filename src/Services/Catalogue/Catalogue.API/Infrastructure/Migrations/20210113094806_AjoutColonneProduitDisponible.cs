using Microsoft.EntityFrameworkCore.Migrations;

namespace Catalogue.API.Infrastructure.Migrations
{
    public partial class AjoutColonneProduitDisponible : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_available",
                table: "produits",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_available",
                table: "produits");
        }
    }
}
