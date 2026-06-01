using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace web.Migrations
{
    /// <inheritdoc />
    public partial class ExpandMenu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "DisplayOrder", "Name" },
                values: new object[] { 4, 4, "Desserts" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BasePrice", "CategoryId", "Description", "ImageUrl", "IsAvailable", "IsCustomizable", "Name" },
                values: new object[,]
                {
                    { 15, 14.49m, 1, "Ham, mushrooms, artichokes, olives and mozzarella.", "/images/menu/capricciosa.jpg", true, true, "Capricciosa" },
                    { 16, 10.99m, 1, "Tomato, garlic, oregano and olive oil — no cheese.", "/images/menu/marinara.jpg", true, true, "Marinara" },
                    { 17, 13.99m, 1, "Folded pizza stuffed with ham, mushrooms and mozzarella.", "/images/menu/calzone.jpg", true, true, "Calzone" },
                    { 18, 6.99m, 2, "Breaded mozzarella with a marinara dip.", "/images/menu/mozzarella-sticks.jpg", true, false, "Mozzarella Sticks" },
                    { 19, 5.49m, 2, "Crispy battered onion rings.", "/images/menu/onion-rings.jpg", true, false, "Onion Rings" },
                    { 20, 2.49m, 3, "Chilled lemon-lime soda.", "/images/menu/sprite.jpg", true, false, "Sprite (500 ml)" },
                    { 21, 2.99m, 3, "Freshly brewed iced tea with lemon.", "/images/menu/iced-tea.jpg", true, false, "Iced Tea" },
                    { 22, 6.49m, 4, "Classic Italian coffee and mascarpone dessert.", "/images/menu/tiramisu.jpg", true, false, "Tiramisu" },
                    { 23, 5.49m, 4, "Warm fudgy chocolate brownie.", "/images/menu/chocolate-brownie.jpg", true, false, "Chocolate Brownie" },
                    { 24, 6.99m, 4, "New York style cheesecake with berries.", "/images/menu/cheesecake.jpg", true, false, "Cheesecake" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
