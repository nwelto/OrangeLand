using Microsoft.EntityFrameworkCore;
using OrangeLand.Data;
using OrangeLand.Models;
using OrangeLand.DTO;

namespace OrangeLand.API
{
    public class GuestsAPI
    {
        public static void Map(WebApplication app)
        {
            // Post guests
            app.MapPost("/guests", (OrangeLandDbContext db, CreateGuestDTO newGuestDto) =>
            {
                if (string.IsNullOrWhiteSpace(newGuestDto.Name) || string.IsNullOrWhiteSpace(newGuestDto.RVType))
                {
                    return Results.BadRequest("Name and RVType are required fields.");
                }

                var newGuest = new Guests
                {
                    Name = newGuestDto.Name,
                    RVType = newGuestDto.RVType
                };

                db.Guests.Add(newGuest);
                db.SaveChanges();

                return Results.Created($"/guests/{newGuest.Id}", newGuest);
            });

            // Put guests
            app.MapPut("/guests/{guestId}", async (OrangeLandDbContext db, int guestId, UpdateGuestDTO updatedGuestDto) =>
            {
                var guestToUpdate = await db.Guests
                                            .Include(g => g.Reservations) 
                                            .FirstOrDefaultAsync(g => g.Id == guestId);

                if (guestToUpdate == null)
                {
                    return Results.NotFound("Guest not found.");
                }

                if (!string.IsNullOrEmpty(updatedGuestDto.Name) && updatedGuestDto.Name != "string")
                {
                    guestToUpdate.Name = updatedGuestDto.Name;
                }

                if (!string.IsNullOrEmpty(updatedGuestDto.RVType) && updatedGuestDto.RVType != "string")
                {
                    guestToUpdate.RVType = updatedGuestDto.RVType;
                }

                await db.SaveChangesAsync();

                var result = new
                {
                    guestToUpdate.Id,
                    guestToUpdate.Name,
                    guestToUpdate.RVType,
                    Reservations = guestToUpdate.Reservations.Select(r => new
                    {
                        r.Id,
                        r.StartDate,
                        r.EndDate,
                        r.Status
                    })
                };

                return Results.Ok(result);
            });





            // Get guests
            app.MapGet("/guests", (OrangeLandDbContext db) =>
            {
                var guests = db.Guests.Select(g => new
                {
                    GuestId = g.Id,
                    Name = g.Name,
                    RVType = g.RVType
                }).ToList();

                return Results.Ok(guests);
            });

            // Get guest by ID
            app.MapGet("/guests/{guestId}", (OrangeLandDbContext db, int guestId) =>
            {
                var guest = db.Guests.Find(guestId);

                if (guest == null)
                {
                    return Results.NotFound("Guest not found.");
                }

                var guestDetails = new
                {
                    GuestId = guest.Id,
                    Name = guest.Name,
                    RVType = guest.RVType
                };

                return Results.Ok(guestDetails);
            });


            // Delete guest by ID
            app.MapDelete("/guests/{guestId}", (OrangeLandDbContext db, int guestId) =>
            {
                var guestToDelete = db.Guests.FirstOrDefault(g => g.Id == guestId);

                if (guestToDelete == null)
                {
                    return Results.NotFound("Guest not found.");
                }

                db.Guests.Remove(guestToDelete);
                db.SaveChanges();

                return Results.Ok("Guest successfully deleted.");
            });

        }
    }
}


