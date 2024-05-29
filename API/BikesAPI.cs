using Microsoft.EntityFrameworkCore;
using OrangeLand.Data;
using OrangeLand.Models;
using OrangeLand.DTO;

namespace OrangeLand.API
{
    public class BikesAPI
    {
        public static void Map(WebApplication app)
        {

            // Get all bikes
            app.MapGet("/bikes", async (OrangeLandDbContext db) =>
            {
                var bikes = await db.Bikes
                    .Include(b => b.BikeRentals)
                    .ToListAsync();

                var bikesDtos = bikes.Select(b => new
                {
                    b.Id,
                    b.Type,
                    b.RentalFee,
                    b.IsAvailable,
                    BikeRentals = b.BikeRentals.Select(br => new
                    {
                        br.ReservationId
                    }).ToList()
                }).ToList();

                return Results.Ok(bikesDtos);
            });





            // Get a single bike by ID
            app.MapGet("/bikes/{bikeId}", async (OrangeLandDbContext db, int bikeId) =>
            {
                var bike = await db.Bikes
                    .FirstOrDefaultAsync(b => b.Id == bikeId);

                if (bike == null)
                {
                    return Results.NotFound("Bike not found.");
                }

                var bikeDto = new
                {
                    Id = bike.Id,
                    Type = bike.Type,
                    RentalFee = bike.RentalFee,
                    IsAvailable = bike.IsAvailable
                };

                return Results.Ok(bikeDto);
            });

        }
    }
}

