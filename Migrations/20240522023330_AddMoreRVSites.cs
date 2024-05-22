using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OrangeLand.Migrations
{
    /// <inheritdoc />
    public partial class AddMoreRVSites : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "RVSites",
                columns: new[] { "Id", "HasGrassyArea", "IsPullThrough", "SiteNumber" },
                values: new object[,]
                {
                    { 5, true, false, "C1" },
                    { 6, false, true, "C2" },
                    { 7, true, false, "D1" },
                    { 8, false, true, "D2" },
                    { 9, true, false, "E1" },
                    { 10, false, true, "E2" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RVSites",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "RVSites",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "RVSites",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "RVSites",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "RVSites",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "RVSites",
                keyColumn: "Id",
                keyValue: 10);
        }
    }
}
