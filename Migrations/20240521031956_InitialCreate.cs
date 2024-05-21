using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OrangeLand.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bikes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Type = table.Column<string>(type: "text", nullable: false),
                    RentalFee = table.Column<decimal>(type: "numeric", nullable: false),
                    IsAvailable = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bikes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Guests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    RVType = table.Column<string>(type: "text", nullable: false),
                    PreferredSiteId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RVSites",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SiteNumber = table.Column<string>(type: "text", nullable: false),
                    HasGrassyArea = table.Column<bool>(type: "boolean", nullable: false),
                    IsPullThrough = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RVSites", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Phone = table.Column<string>(type: "text", nullable: false),
                    IsAdmin = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    SiteId = table.Column<int>(type: "integer", nullable: false),
                    GuestId = table.Column<int>(type: "integer", nullable: false),
                    StartDate = table.Column<string>(type: "text", nullable: false),
                    EndDate = table.Column<string>(type: "text", nullable: false),
                    NumberOfGuests = table.Column<int>(type: "integer", nullable: false),
                    NumberOfDogs = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservations_Guests_GuestId",
                        column: x => x.GuestId,
                        principalTable: "Guests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservations_RVSites_SiteId",
                        column: x => x.SiteId,
                        principalTable: "RVSites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservations_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BikeRentals",
                columns: table => new
                {
                    ReservationId = table.Column<int>(type: "integer", nullable: false),
                    BikeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BikeRentals", x => new { x.ReservationId, x.BikeId });
                    table.ForeignKey(
                        name: "FK_BikeRentals_Bikes_BikeId",
                        column: x => x.BikeId,
                        principalTable: "Bikes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BikeRentals_Reservations_ReservationId",
                        column: x => x.ReservationId,
                        principalTable: "Reservations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Bikes",
                columns: new[] { "Id", "IsAvailable", "RentalFee", "Type" },
                values: new object[,]
                {
                    { 1, true, 15.00m, "Mountain Bike" },
                    { 2, true, 12.00m, "Road Bike" },
                    { 3, true, 10.00m, "Hybrid Bike" },
                    { 4, true, 20.00m, "Electric Bike" }
                });

            migrationBuilder.InsertData(
                table: "Guests",
                columns: new[] { "Id", "Name", "PreferredSiteId", "RVType" },
                values: new object[,]
                {
                    { 1, "Amuro Ray", 1, "Mobile Suit" },
                    { 2, "Char Aznable", 2, "Mobile Suit" },
                    { 3, "Kamille Bidan", 3, "Mobile Suit" },
                    { 4, "Haman Karn", 4, "Mobile Suit" }
                });

            migrationBuilder.InsertData(
                table: "RVSites",
                columns: new[] { "Id", "HasGrassyArea", "IsPullThrough", "SiteNumber" },
                values: new object[,]
                {
                    { 1, true, false, "A1" },
                    { 2, false, true, "A2" },
                    { 3, true, false, "B1" },
                    { 4, false, true, "B2" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "IsAdmin", "Name", "Phone" },
                values: new object[,]
                {
                    { 1, "manager@orangland.com", true, "Manager", "123-456-7890" },
                    { 2, "employee1@orangland.com", false, "Employee1", "123-456-7891" },
                    { 3, "employee2@orangland.com", false, "Employee2", "123-456-7892" },
                    { 4, "employee3@orangland.com", false, "Employee3", "123-456-7893" }
                });

            migrationBuilder.InsertData(
                table: "Reservations",
                columns: new[] { "Id", "EndDate", "GuestId", "NumberOfDogs", "NumberOfGuests", "SiteId", "StartDate", "Status", "UserId" },
                values: new object[,]
                {
                    { 1, "2024-06-10", 1, 0, 1, 1, "2024-06-01", "Confirmed", 2 },
                    { 2, "2024-06-11", 2, 0, 1, 2, "2024-06-02", "Confirmed", 3 },
                    { 3, "2024-06-12", 3, 0, 1, 3, "2024-06-03", "Confirmed", 4 },
                    { 4, "2024-06-13", 4, 0, 1, 4, "2024-06-04", "Confirmed", 2 }
                });

            migrationBuilder.InsertData(
                table: "BikeRentals",
                columns: new[] { "BikeId", "ReservationId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 3, 3 },
                    { 4, 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BikeRentals_BikeId",
                table: "BikeRentals",
                column: "BikeId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_GuestId",
                table: "Reservations",
                column: "GuestId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_SiteId",
                table: "Reservations",
                column: "SiteId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_UserId",
                table: "Reservations",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BikeRentals");

            migrationBuilder.DropTable(
                name: "Bikes");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "Guests");

            migrationBuilder.DropTable(
                name: "RVSites");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
