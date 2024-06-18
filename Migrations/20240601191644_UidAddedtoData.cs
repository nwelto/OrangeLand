using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrangeLand.Migrations
{
    /// <inheritdoc />
    public partial class UidAddedtoData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PreferredSiteId",
                table: "Guests");

            migrationBuilder.AddColumn<string>(
                name: "Uid",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Uid",
                value: "DJNoS94RYXSgpS0jTW7RSVlWyCG3");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "Uid",
                value: "pVt45Of2j2ThgpcIPKruq1pKn4A2");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "Uid",
                value: "udUYrA1rU1huTDv7dIsRYDcdLwl2");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "Uid",
                value: "vaO8Bo1J2SVL7O2grpe1r0Lef9R2");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "IsAdmin", "Name", "Phone", "Uid" },
                values: new object[] { 8, "nathopp@gmail.com", true, "Nathan Welton", "573-380-5105", "R14xkEO4dWbOISMu0hnPkNCBpSt1" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DropColumn(
                name: "Uid",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "PreferredSiteId",
                table: "Guests",
                type: "integer",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Guests",
                keyColumn: "Id",
                keyValue: 1,
                column: "PreferredSiteId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Guests",
                keyColumn: "Id",
                keyValue: 2,
                column: "PreferredSiteId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Guests",
                keyColumn: "Id",
                keyValue: 3,
                column: "PreferredSiteId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Guests",
                keyColumn: "Id",
                keyValue: 4,
                column: "PreferredSiteId",
                value: 4);
        }
    }
}
