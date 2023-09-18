using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce.Migrations
{
    public partial class ReconfigeredResturentTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photo",
                table: "Restaurants");

            migrationBuilder.AddColumn<byte[]>(
                name: "Poster",
                table: "Restaurants",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ResAdmin",
                table: "Restaurants",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Restaurants_ResAdmin",
                table: "Restaurants",
                column: "ResAdmin",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurants_AspNetUsers_ResAdmin",
                table: "Restaurants",
                column: "ResAdmin",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Restaurants_AspNetUsers_ResAdmin",
                table: "Restaurants");

            migrationBuilder.DropIndex(
                name: "IX_Restaurants_ResAdmin",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "Poster",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "ResAdmin",
                table: "Restaurants");

            migrationBuilder.AddColumn<string>(
                name: "Photo",
                table: "Restaurants",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
