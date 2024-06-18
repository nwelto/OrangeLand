using Microsoft.EntityFrameworkCore;
using OrangeLand.Models;

namespace OrangeLand.Data
{
    public class OrangeLandDbContext : DbContext
    {
        public DbSet<Users> Users { get; set; }
        public DbSet<Guests> Guests { get; set; }
        public DbSet<Reservations> Reservations { get; set; }
        public DbSet<RVSites> RVSites { get; set; }
        public DbSet<Bikes> Bikes { get; set; }
        public DbSet<BikeRentals> BikeRentals { get; set; }

        public OrangeLandDbContext(DbContextOptions<OrangeLandDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed data for Users
            modelBuilder.Entity<Users>().HasData(new Users[]
            {
        new Users { Id = 1, Uid = "DJNoS94RYXSgpS0jTW7RSVlWyCG3", Name = "Manager", Email = "manager@orangland.com", IsAdmin = true },
        new Users { Id = 2, Uid = "pVt45Of2j2ThgpcIPKruq1pKn4A2", Name = "Employee1", Email = "employee1@orangland.com", IsAdmin = false },
        new Users { Id = 3, Uid = "udUYrA1rU1huTDv7dIsRYDcdLwl2", Name = "Employee2", Email = "employee2@orangland.com", IsAdmin = false },
        new Users { Id = 4, Uid = "vaO8Bo1J2SVL7O2grpe1r0Lef9R2", Name = "Employee3", Email = "employee3@orangland.com",  IsAdmin = false },
        new Users { Id = 8, Uid = "R14xkEO4dWbOISMu0hnPkNCBpSt1", Name = "Nathan Welton", Email = "nathopp@gmail.com",  IsAdmin = true }
            });

            // Seed data for Guests
            modelBuilder.Entity<Guests>().HasData(new Guests[]
            {
        new Guests { Id = 1, Name = "Amuro Ray", RVType = "Mobile Suit", PhoneNumber = "123-456-7890", Email = "amuro.ray@example.com" },
        new Guests { Id = 2, Name = "Char Aznable", RVType = "Mobile Suit", PhoneNumber = "234-567-8901", Email = "char.aznable@example.com" },
        new Guests { Id = 3, Name = "Kamille Bidan", RVType = "Mobile Suit", PhoneNumber = "345-678-9012", Email = "kamille.bidan@example.com" },
        new Guests { Id = 4, Name = "Haman Karn", RVType = "Mobile Suit", PhoneNumber = "456-789-0123", Email = "haman.karn@example.com" }
            });

            // Seed data for RVSites
            modelBuilder.Entity<RVSites>().HasData(new RVSites[]
            {
        new RVSites { Id = 1, SiteNumber = "A1", HasGrassyArea = true, IsPullThrough = false },
        new RVSites { Id = 2, SiteNumber = "A2", HasGrassyArea = false, IsPullThrough = true },
        new RVSites { Id = 3, SiteNumber = "B1", HasGrassyArea = true, IsPullThrough = false },
        new RVSites { Id = 4, SiteNumber = "B2", HasGrassyArea = false, IsPullThrough = true },
        new RVSites { Id = 5, SiteNumber = "C1", HasGrassyArea = true, IsPullThrough = false },
        new RVSites { Id = 6, SiteNumber = "C2", HasGrassyArea = false, IsPullThrough = true },
        new RVSites { Id = 7, SiteNumber = "D1", HasGrassyArea = true, IsPullThrough = false },
        new RVSites { Id = 8, SiteNumber = "D2", HasGrassyArea = false, IsPullThrough = true },
        new RVSites { Id = 9, SiteNumber = "E1", HasGrassyArea = true, IsPullThrough = false },
        new RVSites { Id = 10, SiteNumber = "E2", HasGrassyArea = false, IsPullThrough = true }
            });

            modelBuilder.Entity<Reservations>().HasData(new Reservations[]
            {
    new Reservations { Id = 1, UserId = 2, SiteId = 1, GuestId = 1, StartDate = "2024-06-01", EndDate = "2024-06-10", NumberOfGuests = 1, NumberOfDogs = 0, Status = ReservationStatus.Confirmed },
    new Reservations { Id = 2, UserId = 3, SiteId = 2, GuestId = 2, StartDate = "2024-06-02", EndDate = "2024-06-11", NumberOfGuests = 1, NumberOfDogs = 0, Status = ReservationStatus.Confirmed },
    new Reservations { Id = 3, UserId = 4, SiteId = 3, GuestId = 3, StartDate = "2024-06-03", EndDate = "2024-06-12", NumberOfGuests = 1, NumberOfDogs = 0, Status = ReservationStatus.Confirmed },
    new Reservations { Id = 4, UserId = 2, SiteId = 4, GuestId = 4, StartDate = "2024-06-04", EndDate = "2024-06-13", NumberOfGuests = 1, NumberOfDogs = 0, Status = ReservationStatus.Confirmed }
            });


            // Seed data for Bikes
            modelBuilder.Entity<Bikes>().HasData(new Bikes[]
            {

                new Bikes { Id = 1, Type = "Mountain Bike", RentalFee = 15.00m, IsAvailable = true },
                new Bikes { Id = 2, Type = "Mountain Bike", RentalFee = 15.00m, IsAvailable = true },
                new Bikes { Id = 3, Type = "Mountain Bike", RentalFee = 15.00m, IsAvailable = true },
                new Bikes { Id = 4, Type = "Mountain Bike", RentalFee = 15.00m, IsAvailable = true },
                new Bikes { Id = 5, Type = "Mountain Bike", RentalFee = 15.00m, IsAvailable = true },
                new Bikes { Id = 6, Type = "Road Bike", RentalFee = 12.00m, IsAvailable = true },
                new Bikes { Id = 7, Type = "Road Bike", RentalFee = 12.00m, IsAvailable = true },
                new Bikes { Id = 8, Type = "Road Bike", RentalFee = 12.00m, IsAvailable = true },
                new Bikes { Id = 9, Type = "Road Bike", RentalFee = 12.00m, IsAvailable = true },
                new Bikes { Id = 10, Type = "Road Bike", RentalFee = 12.00m, IsAvailable = true },
                new Bikes { Id = 11, Type = "Hybrid Bike", RentalFee = 10.00m, IsAvailable = true },
                new Bikes { Id = 12, Type = "Hybrid Bike", RentalFee = 10.00m, IsAvailable = true },
                new Bikes { Id = 13, Type = "Hybrid Bike", RentalFee = 10.00m, IsAvailable = true },
                new Bikes { Id = 14, Type = "Hybrid Bike", RentalFee = 10.00m, IsAvailable = true },
                new Bikes { Id = 15, Type = "Hybrid Bike", RentalFee = 10.00m, IsAvailable = true },
                new Bikes { Id = 16, Type = "Electric Bike", RentalFee = 20.00m, IsAvailable = true },
                new Bikes { Id = 17, Type = "Electric Bike", RentalFee = 20.00m, IsAvailable = true },
                new Bikes { Id = 18, Type = "Electric Bike", RentalFee = 20.00m, IsAvailable = true },
                new Bikes { Id = 19, Type = "Electric Bike", RentalFee = 20.00m, IsAvailable = true },
                new Bikes { Id = 20, Type = "Electric Bike", RentalFee = 20.00m, IsAvailable = true }
            });

            // Seed data for BikeRentals
            modelBuilder.Entity<BikeRentals>().HasData(new BikeRentals[]
            {
        new BikeRentals { ReservationId = 1, BikeId = 1 },
        new BikeRentals { ReservationId = 2, BikeId = 2 },
        new BikeRentals { ReservationId = 3, BikeId = 3 },
        new BikeRentals { ReservationId = 4, BikeId = 4 }
            });

            modelBuilder.Entity<BikeRentals>()
                .HasKey(br => new { br.ReservationId, br.BikeId });

        }

    }
}









