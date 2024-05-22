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

            //Get all bikes
            app.MapGet("/bikes", (OrangeLandDbContext db) =>
            {
                var bikes = db.Bikes
                    .Select(b => new BikesDTO
                    {
                        Id = b.Id,
                        Type = b.Type,
                        RentalFee = b.RentalFee,
                        IsAvailable = b.IsAvailable
                    })
                    .ToList();

                return Results.Ok(bikes);
            });
            //Get single bike
            app.MapGet("/bikes/{bikeId}", (OrangeLandDbContext db, int bikeId) =>
            {
                var bike = db.Bikes
                    .Where(b => b.Id == bikeId)
                    .Select(b => new BikesDTO
                    {
                        Id = b.Id,
                        Type = b.Type,
                        RentalFee = b.RentalFee,
                        IsAvailable = b.IsAvailable
                    })
                    .FirstOrDefault();

                if (bike == null)
                {
                    return Results.NotFound("Bike not found.");
                }

                return Results.Ok(bike);
            });
        }
    }
}

