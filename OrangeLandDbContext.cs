using Microsoft.EntityFrameworkCore;
using OrangeLand.Models;

namespace OrangeLand.Data
{
    public class OrangeLandDbContext : DbContext
    {
        public OrangeLandDbContext(DbContextOptions<OrangeLandDbContext> options)
            : base(options)
        {
        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Guests> Guests { get; set; }
        public DbSet<Reservations> Reservations { get; set; }
        public DbSet<RVSites> RVSites { get; set; }
        public DbSet<Bikes> Bikes { get; set; }
        public DbSet<BikeRentals> BikeRentals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BikeRentals>()
                .HasKey(br => new { br.ReservationId, br.BikeId });

            // Seed data for Users
            modelBuilder.Entity<Users>().HasData(
                new Users { UserId = 1, Name = "Manager", Email = "manager@orangland.com", Phone = "123-456-7890", Role = "Manager" },
                new Users { UserId = 2, Name = "Employee1", Email = "employee1@orangland.com", Phone = "123-456-7891", Role = "Employee" },
                new Users { UserId = 3, Name = "Employee2", Email = "employee2@orangland.com", Phone = "123-456-7892", Role = "Employee" },
                new Users { UserId = 4, Name = "Employee3", Email = "employee3@orangland.com", Phone = "123-456-7893", Role = "Employee" }
            );

            // Seed data for Guests
            modelBuilder.Entity<Guests>().HasData(
                new Guests { GuestId = 1, Name = "Amuro Ray", RVType = "Mobile Suit", PreferredSiteId = 1 },
                new Guests { GuestId = 2, Name = "Char Aznable", RVType = "Mobile Suit", PreferredSiteId = 2 },
                new Guests { GuestId = 3, Name = "Kamille Bidan", RVType = "Mobile Suit", PreferredSiteId = 3 },
                new Guests { GuestId = 4, Name = "Haman Karn", RVType = "Mobile Suit", PreferredSiteId = 4 }
            );

            // Seed data for RVSites
            modelBuilder.Entity<RVSites>().HasData(
                new RVSites { SiteId = 1, SiteNumber = "A1", HasGrassyArea = true, IsPullThrough = false },
                new RVSites { SiteId = 2, SiteNumber = "A2", HasGrassyArea = false, IsPullThrough = true },
                new RVSites { SiteId = 3, SiteNumber = "B1", HasGrassyArea = true, IsPullThrough = false },
                new RVSites { SiteId = 4, SiteNumber = "B2", HasGrassyArea = false, IsPullThrough = true }
            );

            // Seed data for Reservations
            modelBuilder.Entity<Reservations>().HasData(
                new Reservations { ReservationId = 1, UserId = 2, SiteId = 1, GuestId = 1, StartDate = "2024-06-01", EndDate = "2024-06-10", NumberOfGuests = 1, NumberOfDogs = 0, Status = "Confirmed" },
                new Reservations { ReservationId = 2, UserId = 3, SiteId = 2, GuestId = 2, StartDate = "2024-06-02", EndDate = "2024-06-11", NumberOfGuests = 1, NumberOfDogs = 0, Status = "Confirmed" },
                new Reservations { ReservationId = 3, UserId = 4, SiteId = 3, GuestId = 3, StartDate = "2024-06-03", EndDate = "2024-06-12", NumberOfGuests = 1, NumberOfDogs = 0, Status = "Confirmed" },
                new Reservations { ReservationId = 4, UserId = 2, SiteId = 4, GuestId = 4, StartDate = "2024-06-04", EndDate = "2024-06-13", NumberOfGuests = 1, NumberOfDogs = 0, Status = "Confirmed" }
            );

            // Seed data for Bikes
            modelBuilder.Entity<Bikes>().HasData(
                new Bikes { BikeId = 1, Type = "Mountain Bike", RentalFee = 15.00m, IsAvailable = true },
                new Bikes { BikeId = 2, Type = "Road Bike", RentalFee = 12.00m, IsAvailable = true },
                new Bikes { BikeId = 3, Type = "Hybrid Bike", RentalFee = 10.00m, IsAvailable = true },
                new Bikes { BikeId = 4, Type = "Electric Bike", RentalFee = 20.00m, IsAvailable = true }
            );

            // Seed data for BikeRentals
            modelBuilder.Entity<BikeRentals>().HasData(
                new BikeRentals { ReservationId = 1, BikeId = 1 },
                new BikeRentals { ReservationId = 2, BikeId = 2 },
                new BikeRentals { ReservationId = 3, BikeId = 3 },
                new BikeRentals { ReservationId = 4, BikeId = 4 }
            );
        }
    }
}


