using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce.Migrations
{
    public partial class restire : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           /* migrationBuilder.RenameColumn(
                name: "Photo",
                table: "MenuItems",
                newName: "image");
           
            migrationBuilder.AddColumn<bool>(
                name: "Offer",
                table: "MenuItems",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "AdminCheck",
                table: "AspNetUsers",
                type: "bit",
                nullable: true);*/
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
           /* migrationBuilder.DropColumn(
                name: "Offer",
                table: "MenuItems");

            migrationBuilder.DropColumn(
                name: "AdminCheck",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "image",
                table: "MenuItems",
                newName: "Photo");*/
        }
    }
}
