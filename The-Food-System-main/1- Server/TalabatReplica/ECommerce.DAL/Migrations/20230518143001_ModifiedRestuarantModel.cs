using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce.Migrations
{
    public partial class ModifiedRestuarantModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Restaurants_AspNetUsers_ResAdmin",
                table: "Restaurants");

            migrationBuilder.RenameColumn(
                name: "ResAdmin",
                table: "Restaurants",
                newName: "ResAdminID");

            migrationBuilder.RenameIndex(
                name: "IX_Restaurants_ResAdmin",
                table: "Restaurants",
                newName: "IX_Restaurants_ResAdminID");

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurants_AspNetUsers_ResAdminID",
                table: "Restaurants",
                column: "ResAdminID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Restaurants_AspNetUsers_ResAdminID",
                table: "Restaurants");

            migrationBuilder.RenameColumn(
                name: "ResAdminID",
                table: "Restaurants",
                newName: "ResAdmin");

            migrationBuilder.RenameIndex(
                name: "IX_Restaurants_ResAdminID",
                table: "Restaurants",
                newName: "IX_Restaurants_ResAdmin");

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurants_AspNetUsers_ResAdmin",
                table: "Restaurants",
                column: "ResAdmin",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
