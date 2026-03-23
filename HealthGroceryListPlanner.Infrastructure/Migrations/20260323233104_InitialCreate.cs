using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HealthGroceryListPlanner.Infrastructure.Migrations
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
                    Emoji = table.Column<string>(type: "TEXT", nullable: false),
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
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Role = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    Unit = table.Column<int>(type: "INTEGER", nullable: true),
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
                columns: new[] { "Id", "Emoji", "ImageUrl", "Name" },
                values: new object[,]
                {
                    { 1, "", "/images/vegetables.jpg", "Vegetables" },
                    { 2, "", "/images/fruits.jpg", "Fruits and Berries" },
                    { 3, "", "/images/protein.jpg", "Protein" },
                    { 4, "", "/images/dairy.jpg", "Dairy" },
                    { 5, "", "/images/grains.jpg", "Whole Grains" },
                    { 6, "", "/images/nuts.jpg", "Nuts & Seeds" },
                    { 7, "", "/images/fats.jpg", "Healthy Fats" },
                    { 8, "", "/images/beverages.jpg", "Beverages" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Emoji", "IsPurchased", "Name", "Quantity", "ShoppingListId", "Unit" },
                values: new object[,]
                {
                    { 201, 1, "🥕", false, "Carrot", 1, null, 0 },
                    { 202, 1, "🥔", false, "Potato", 1, null, 0 },
                    { 203, 1, "🍅", false, "Tomato", 1, null, 0 },
                    { 204, 1, "🥒", false, "Cucumber", 1, null, 0 },
                    { 205, 1, "🧅", false, "Onion", 1, null, 0 },
                    { 206, 1, "🧄", false, "Garlic", 1, null, 0 },
                    { 207, 1, "🍆", false, "Eggplant", 1, null, 0 },
                    { 208, 1, "🌽", false, "Corn", 1, null, 0 },
                    { 301, 2, "🍎", false, "Apple", 1, null, 0 },
                    { 302, 2, "🍌", false, "Banana", 1, null, 0 },
                    { 303, 2, "🍊", false, "Orange", 1, null, 0 },
                    { 304, 2, "🍓", false, "Strawberry", 1, null, 0 },
                    { 305, 2, "🍇", false, "Grapes", 1, null, 0 },
                    { 401, 3, "🍗", false, "Chicken Breast", 1, null, 0 },
                    { 402, 3, "🐟", false, "Salmon", 1, null, 0 },
                    { 403, 3, "🥚", false, "Eggs", 1, null, 0 },
                    { 404, 3, "🧊", false, "Tofu", 1, null, 0 },
                    { 405, 3, "🫘", false, "Beans", 1, null, 0 },
                    { 501, 4, "🥛", false, "Milk", 1, null, 0 },
                    { 502, 4, "🧀", false, "Cheese", 1, null, 0 },
                    { 503, 4, "🥣", false, "Yogurt", 1, null, 0 },
                    { 504, 4, "🧈", false, "Butter", 1, null, 0 },
                    { 505, 4, "🥛", false, "Cottage Cheese", 1, null, 0 },
                    { 601, 5, "🍚", false, "Brown Rice", 1, null, 0 },
                    { 602, 5, "🥣", false, "Oats", 1, null, 0 },
                    { 603, 5, "🍚", false, "Quinoa", 1, null, 0 },
                    { 604, 5, "🍞", false, "Whole Wheat Bread", 1, null, 0 },
                    { 605, 5, "🌾", false, "Barley", 1, null, 0 },
                    { 701, 6, "🌰", false, "Almonds", 1, null, 0 },
                    { 702, 6, "🌰", false, "Walnuts", 1, null, 0 },
                    { 703, 6, "🥜", false, "Cashews", 1, null, 0 },
                    { 704, 6, "🌱", false, "Chia Seeds", 1, null, 0 },
                    { 705, 6, "🌱", false, "Flax Seeds", 1, null, 0 },
                    { 801, 7, "🥑", false, "Avocado", 1, null, 0 },
                    { 802, 7, "🫒", false, "Olive Oil", 1, null, 0 },
                    { 803, 7, "🥥", false, "Coconut Oil", 1, null, 0 },
                    { 804, 7, "🍫", false, "Dark Chocolate", 1, null, 0 },
                    { 805, 7, "🥜", false, "Peanut Butter", 1, null, 0 },
                    { 901, 8, "☕", false, "Coffee", 1, null, 0 },
                    { 902, 8, "🍵", false, "Green Tea", 1, null, 0 },
                    { 903, 8, "🍵", false, "Black Tea", 1, null, 0 },
                    { 904, 8, "🍊", false, "Orange Juice", 1, null, 0 },
                    { 905, 8, "🥤", false, "Smoothie", 1, null, 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ShoppingListId",
                table: "Products",
                column: "ShoppingListId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "ShoppingLists");
        }
    }
}
