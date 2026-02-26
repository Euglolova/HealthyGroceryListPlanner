using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthGroceryListPlanner.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddShoppingList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ShoppingListId",
                table: "Products",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ShoppingLists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingLists", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_ShoppingListId",
                table: "Products",
                column: "ShoppingListId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ShoppingLists_ShoppingListId",
                table: "Products",
                column: "ShoppingListId",
                principalTable: "ShoppingLists",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ShoppingLists_ShoppingListId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "ShoppingLists");

            migrationBuilder.DropIndex(
                name: "IX_Products_ShoppingListId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ShoppingListId",
                table: "Products");
        }
    }
}
