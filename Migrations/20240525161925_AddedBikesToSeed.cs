using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OrangeLand.Migrations
{
    /// <inheritdoc />
    public partial class AddedBikesToSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Bikes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "RentalFee", "Type" },
                values: new object[] { 15.00m, "Mountain Bike" });

            migrationBuilder.UpdateData(
                table: "Bikes",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "RentalFee", "Type" },
                values: new object[] { 15.00m, "Mountain Bike" });

            migrationBuilder.UpdateData(
                table: "Bikes",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "RentalFee", "Type" },
                values: new object[] { 15.00m, "Mountain Bike" });

            migrationBuilder.InsertData(
                table: "Bikes",
                columns: new[] { "Id", "IsAvailable", "RentalFee", "Type" },
                values: new object[,]
                {
                    { 5, true, 15.00m, "Mountain Bike" },
                    { 6, true, 12.00m, "Road Bike" },
                    { 7, true, 12.00m, "Road Bike" },
                    { 8, true, 12.00m, "Road Bike" },
                    { 9, true, 12.00m, "Road Bike" },
                    { 10, true, 12.00m, "Road Bike" },
                    { 11, true, 10.00m, "Hybrid Bike" },
                    { 12, true, 10.00m, "Hybrid Bike" },
                    { 13, true, 10.00m, "Hybrid Bike" },
                    { 14, true, 10.00m, "Hybrid Bike" },
                    { 15, true, 10.00m, "Hybrid Bike" },
                    { 16, true, 20.00m, "Electric Bike" },
                    { 17, true, 20.00m, "Electric Bike" },
                    { 18, true, 20.00m, "Electric Bike" },
                    { 19, true, 20.00m, "Electric Bike" },
                    { 20, true, 20.00m, "Electric Bike" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Bikes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Bikes",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Bikes",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Bikes",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Bikes",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Bikes",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Bikes",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Bikes",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Bikes",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Bikes",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Bikes",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Bikes",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Bikes",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Bikes",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Bikes",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Bikes",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.UpdateData(
                table: "Bikes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "RentalFee", "Type" },
                values: new object[] { 12.00m, "Road Bike" });

            migrationBuilder.UpdateData(
                table: "Bikes",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "RentalFee", "Type" },
                values: new object[] { 10.00m, "Hybrid Bike" });

            migrationBuilder.UpdateData(
                table: "Bikes",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "RentalFee", "Type" },
                values: new object[] { 20.00m, "Electric Bike" });
        }
    }
}
