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
                new Users { Id = 1, Name = "Manager", Email = "manager@orangland.com", Phone = "123-456-7890", Role = "Manager" },
                new Users { Id = 2, Name = "Employee1", Email = "employee1@orangland.com", Phone = "123-456-7891", Role = "Employee" },
                new Users { Id = 3, Name = "Employee2", Email = "employee2@orangland.com", Phone = "123-456-7892", Role = "Employee" },
                new Users { Id = 4, Name = "Employee3", Email = "employee3@orangland.com", Phone = "123-456-7893", Role = "Employee" }
            });

            // Seed data for Guests
            modelBuilder.Entity<Guests>().HasData(new Guests[]
            {
                new Guests { Id = 1, Name = "Amuro Ray", RVType = "Mobile Suit", PreferredSiteId = 1 },
                new Guests { Id = 2, Name = "Char Aznable", RVType = "Mobile Suit", PreferredSiteId = 2 },
                new Guests { Id = 3, Name = "Kamille Bidan", RVType = "Mobile Suit", PreferredSiteId = 3 },
                new Guests { Id = 4, Name = "Haman Karn", RVType = "Mobile Suit", PreferredSiteId = 4 }
            });

            // Seed data for RVSites
            modelBuilder.Entity<RVSites>().HasData(new RVSites[]
            {
                new RVSites { Id = 1, SiteNumber = "A1", HasGrassyArea = true, IsPullThrough = false },
                new RVSites { Id = 2, SiteNumber = "A2", HasGrassyArea = false, IsPullThrough = true },
                new RVSites { Id = 3, SiteNumber = "B1", HasGrassyArea = true, IsPullThrough = false },
                new RVSites { Id = 4, SiteNumber = "B2", HasGrassyArea = false, IsPullThrough = true }
            });

            // Seed data for Reservations
            modelBuilder.Entity<Reservations>().HasData(new Reservations[]
            {
                new Reservations { Id = 1, UserId = 2, SiteId = 1, GuestId = 1, StartDate = "2024-06-01", EndDate = "2024-06-10", NumberOfGuests = 1, NumberOfDogs = 0, Status = "Confirmed" },
                new Reservations { Id = 2, UserId = 3, SiteId = 2, GuestId = 2, StartDate = "2024-06-02", EndDate = "2024-06-11", NumberOfGuests = 1, NumberOfDogs = 0, Status = "Confirmed" },
                new Reservations { Id = 3, UserId = 4, SiteId = 3, GuestId = 3, StartDate = "2024-06-03", EndDate = "2024-06-12", NumberOfGuests = 1, NumberOfDogs = 0, Status = "Confirmed" },
                new Reservations { Id = 4, UserId = 2, SiteId = 4, GuestId = 4, StartDate = "2024-06-04", EndDate = "2024-06-13", NumberOfGuests = 1, NumberOfDogs = 0, Status = "Confirmed" }
            });

            // Seed data for Bikes
            modelBuilder.Entity<Bikes>().HasData(new Bikes[]
            {
                new Bikes { Id = 1, Type = "Mountain Bike", RentalFee = 15.00m, IsAvailable = true },
                new Bikes { Id = 2, Type = "Road Bike", RentalFee = 12.00m, IsAvailable = true },
                new Bikes { Id = 3, Type = "Hybrid Bike", RentalFee = 10.00m, IsAvailable = true },
                new Bikes { Id = 4, Type = "Electric Bike", RentalFee = 20.00m, IsAvailable = true }
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








