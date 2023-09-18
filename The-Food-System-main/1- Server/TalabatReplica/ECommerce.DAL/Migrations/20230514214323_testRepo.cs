using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce.Migrations
{
    public partial class testRepo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuItems_Resturants_ResturantID",
                table: "MenuItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Resturants",
                table: "Resturants");

            migrationBuilder.RenameTable(
                name: "Resturants",
                newName: "Restaurants");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Restaurants",
                table: "Restaurants",
                column: "ResturantID");

            migrationBuilder.CreateTable(
                name: "Tests",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tests", x => x.ID);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItems_Restaurants_ResturantID",
                table: "MenuItems",
                column: "ResturantID",
                principalTable: "Restaurants",
                principalColumn: "ResturantID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuItems_Restaurants_ResturantID",
                table: "MenuItems");

            migrationBuilder.DropTable(
                name: "Tests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Restaurants",
                table: "Restaurants");

            migrationBuilder.RenameTable(
                name: "Restaurants",
                newName: "Resturants");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Resturants",
                table: "Resturants",
                column: "ResturantID");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItems_Resturants_ResturantID",
                table: "MenuItems",
                column: "ResturantID",
                principalTable: "Resturants",
                principalColumn: "ResturantID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
