using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HealthGroceryListPlanner.Web.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    ImageUrl = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Quantity = table.Column<decimal>(type: "TEXT", nullable: false),
                    Unit = table.Column<int>(type: "INTEGER", nullable: false),
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    IsPurchased = table.Column<bool>(type: "INTEGER", nullable: false),
                    Emoji = table.Column<string>(type: "TEXT", nullable: false),
                    ShoppingListId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_ShoppingLists_ShoppingListId",
                        column: x => x.ShoppingListId,
                        principalTable: "ShoppingLists",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "ImageUrl", "Name" },
                values: new object[,]
                {
                    { 1, "/images/vegetables.jpg", "Vegetables" },
                    { 2, "/images/fruits.jpg", "Fruits" },
                    { 3, "/images/protein.jpg", "Protein" },
                    { 4, "/images/dairy.jpg", "Dairy" },
                    { 5, "/images/grains.jpg", "Whole Grains" },
                    { 6, "/images/nuts.jpg", "Nuts & Seeds" },
                    { 7, "/images/fats.jpg", "Healthy Fats" },
                    { 8, "/images/beverages.jpg", "Beverages" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Emoji", "IsPurchased", "Name", "Quantity", "ShoppingListId", "Unit" },
                values: new object[,]
                {
                    { 201, 1, "🥕", false, "Carrot", 1m, null, 0 },
                    { 202, 1, "🥔", false, "Potato", 1m, null, 0 },
                    { 203, 1, "🍅", false, "Tomato", 1m, null, 0 },
                    { 204, 1, "🥒", false, "Cucumber", 1m, null, 0 },
                    { 205, 1, "🧅", false, "Onion", 1m, null, 0 },
                    { 206, 1, "🧄", false, "Garlic", 1m, null, 0 },
                    { 207, 1, "🫑", false, "Bell Pepper", 1m, null, 0 },
                    { 208, 1, "🥦", false, "Broccoli", 1m, null, 0 },
                    { 209, 1, "🥬", false, "Cauliflower", 1m, null, 0 },
                    { 210, 1, "🥬", false, "Spinach", 1m, null, 0 },
                    { 211, 1, "🥬", false, "Lettuce", 1m, null, 0 },
                    { 212, 1, "🥒", false, "Zucchini", 1m, null, 0 },
                    { 213, 1, "🍆", false, "Eggplant", 1m, null, 0 },
                    { 214, 1, "🥬", false, "Cabbage", 1m, null, 0 },
                    { 215, 1, "🥬", false, "Red Cabbage", 1m, null, 0 },
                    { 216, 1, "🥬", false, "Brussels Sprouts", 1m, null, 0 },
                    { 217, 1, "🫛", false, "Green Beans", 1m, null, 0 },
                    { 218, 1, "🫛", false, "Peas", 1m, null, 0 },
                    { 219, 1, "🌽", false, "Corn", 1m, null, 0 },
                    { 220, 1, "🥬", false, "Asparagus", 1m, null, 0 },
                    { 221, 1, "🥬", false, "Celery", 1m, null, 0 },
                    { 222, 1, "🍄", false, "Mushrooms", 1m, null, 0 },
                    { 223, 1, "🍠", false, "Sweet Potato", 1m, null, 0 },
                    { 224, 1, "🥕", false, "Radish", 1m, null, 0 },
                    { 225, 1, "🥕", false, "Beetroot", 1m, null, 0 },
                    { 226, 1, "🥬", false, "Kale", 1m, null, 0 },
                    { 227, 1, "🥬", false, "Arugula", 1m, null, 0 },
                    { 228, 1, "🧅", false, "Leek", 1m, null, 0 },
                    { 229, 1, "🎃", false, "Pumpkin", 1m, null, 0 },
                    { 230, 1, "🎃", false, "Butternut Squash", 1m, null, 0 },
                    { 231, 1, "🥕", false, "Turnip", 1m, null, 0 },
                    { 232, 1, "🥕", false, "Parsnip", 1m, null, 0 },
                    { 233, 1, "🥬", false, "Okra", 1m, null, 0 },
                    { 234, 1, "🌶️", false, "Jalapeño", 1m, null, 0 },
                    { 235, 1, "🥑", false, "Avocado", 1m, null, 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ShoppingListId",
                table: "Products",
                column: "ShoppingListId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "ShoppingLists");
        }
    }
}
