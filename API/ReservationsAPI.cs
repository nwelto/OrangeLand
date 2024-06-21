using Microsoft.EntityFrameworkCore;
using OrangeLand.Data;
using OrangeLand.Models;
using OrangeLand.DTO;

namespace OrangeLand.API
{
    public class ReservationsAPI
    {
        public static void Map(WebApplication app)
        {

            // Create new reservation
            app.MapPost("/reservations", async (OrangeLandDbContext db, ReservationDTO newReservationDto, ILogger<ReservationsAPI> logger) =>
            {
                try
                {
                  
                    var guestExists = await db.Guests.AnyAsync(g => g.Id == newReservationDto.GuestId);
                    var userExists = await db.Users.AnyAsync(u => u.Id == newReservationDto.UserId);
                    var siteExists = await db.RVSites.AnyAsync(s => s.Id == newReservationDto.SiteId);

                    if (!guestExists || !userExists || !siteExists)
                    {
                        logger.LogError("Invalid foreign key reference.");
                        return Results.BadRequest();
                    }

               
                    if (newReservationDto.UserId == null || newReservationDto.SiteId == null || newReservationDto.GuestId == null ||
                        newReservationDto.StartDate == null || newReservationDto.EndDate == null || newReservationDto.Status == null)
                    {
                        logger.LogError("All fields must be provided.");
                        return Results.BadRequest();
                    }

                   
                    var startDate = DateTime.Parse(newReservationDto.StartDate);
                    var endDate = DateTime.Parse(newReservationDto.EndDate);

                    var reservations = await db.Reservations
                        .Where(r => r.SiteId == newReservationDto.SiteId)
                        .ToListAsync();

                    var conflict = reservations
                        .AsEnumerable()
                        .Any(r =>
                            DateTime.Parse(r.StartDate) <= endDate && DateTime.Parse(r.EndDate) >= startDate
                        );

                    if (conflict)
                    {
                        logger.LogError("Reservation conflict detected.");
                        return Results.Conflict();
                    }

                    var reservation = new Reservations
                    {
                        UserId = newReservationDto.UserId.Value,
                        SiteId = newReservationDto.SiteId.Value,
                        GuestId = newReservationDto.GuestId.Value,
                        StartDate = newReservationDto.StartDate,
                        EndDate = newReservationDto.EndDate,
                        NumberOfGuests = newReservationDto.NumberOfGuests ?? 0,
                        NumberOfDogs = newReservationDto.NumberOfDogs ?? 0,
                        Status = newReservationDto.Status.Value
                    };

                    db.Reservations.Add(reservation);
                    await db.SaveChangesAsync();

            
                    var createdReservation = await db.Reservations.FindAsync(reservation.Id);

                    if (createdReservation == null)
                    {
                        logger.LogError("Reservation not found after creation.");
                        return Results.NotFound();
                    }

                   
                    var createdReservationDto = new ReservationDTO
                    {
                        Id = createdReservation.Id,
                        UserId = createdReservation.UserId,
                        SiteId = createdReservation.SiteId,
                        GuestId = createdReservation.GuestId,
                        StartDate = createdReservation.StartDate,
                        EndDate = createdReservation.EndDate,
                        NumberOfGuests = createdReservation.NumberOfGuests,
                        NumberOfDogs = createdReservation.NumberOfDogs,
                        Status = createdReservation.Status
                    };

                    return Results.Created($"/reservations/{createdReservationDto.Id}", createdReservationDto);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred while creating a reservation.");
                    return Results.Problem("An error occurred while creating a reservation.");
                }
            });
            // Get all reservations
            app.MapGet("/reservations", async (OrangeLandDbContext db) =>
            {
                var reservations = await db.Reservations
                    .Include(r => r.BikeRentals)
                    .ThenInclude(br => br.Bike)
                    .ToListAsync();

                var reservationDtos = reservations.Select(r => new ReservationDTO
                {
                    Id = r.Id,
                    UserId = r.UserId,
                    SiteId = r.SiteId,
                    GuestId = r.GuestId,
                    StartDate = r.StartDate,
                    EndDate = r.EndDate,
                    NumberOfGuests = r.NumberOfGuests,
                    NumberOfDogs = r.NumberOfDogs,
                    Status = r.Status,
                    BikeRentals = r.BikeRentals.Select(br => new BikeRentalDTO
                    {
                        ReservationId = br.ReservationId,
                        BikeId = br.BikeId,
                        Bike = new BikeResDTO
                        {
                            RentalFee = br.Bike.RentalFee
                        }
                    }).ToList()
                }).ToList();

                return Results.Ok(reservationDtos);
            });





            // Get a single reservation by ID
            app.MapGet("/reservations/{reservationId}", async (OrangeLandDbContext db, int reservationId) =>
            {
                var reservation = await db.Reservations
                    .Include(r => r.BikeRentals)
                    .ThenInclude(br => br.Bike)
                    .FirstOrDefaultAsync(r => r.Id == reservationId);

                if (reservation == null)
                {
                    return Results.NotFound("Reservation not found.");
                }

                var reservationDto = new ReservationDTO
                {
                    Id = reservation.Id,
                    UserId = reservation.UserId,
                    SiteId = reservation.SiteId,
                    GuestId = reservation.GuestId,
                    StartDate = reservation.StartDate,
                    EndDate = reservation.EndDate,
                    NumberOfGuests = reservation.NumberOfGuests,
                    NumberOfDogs = reservation.NumberOfDogs,
                    Status = reservation.Status,
                    BikeRentals = reservation.BikeRentals.Select(br => new BikeRentalDTO
                    {
                        ReservationId = br.ReservationId,
                        BikeId = br.BikeId,
                        Bike = new BikeResDTO
                        {
                            RentalFee = br.Bike.RentalFee
                        }
                    }).ToList()
                };

                return Results.Ok(reservationDto);
            });






            // Update an existing reservation
            app.MapPut("/reservations/{reservationId}", async (OrangeLandDbContext db, int reservationId, ReservationDTO updatedReservationDto) =>
            {
                var reservation = await db.Reservations
                    .Include(r => r.User)
                    .Include(r => r.Site)
                    .Include(r => r.Guest)
                    .FirstOrDefaultAsync(r => r.Id == reservationId);

                if (reservation == null)
                {
                    return Results.NotFound("Reservation not found.");
                }

                if (updatedReservationDto.GuestId.HasValue && updatedReservationDto.GuestId.Value != 0)
                {
                    var guestExists = await db.Guests.AnyAsync(g => g.Id == updatedReservationDto.GuestId.Value);
                    if (!guestExists)
                    {
                        return Results.BadRequest("Invalid GuestId reference.");
                    }
                    reservation.GuestId = updatedReservationDto.GuestId.Value;
                }

                if (updatedReservationDto.UserId.HasValue && updatedReservationDto.UserId.Value != 0)
                {
                    var userExists = await db.Users.AnyAsync(u => u.Id == updatedReservationDto.UserId.Value);
                    if (!userExists)
                    {
                        return Results.BadRequest("Invalid UserId reference.");
                    }
                    reservation.UserId = updatedReservationDto.UserId.Value;
                }

                if (updatedReservationDto.SiteId.HasValue && updatedReservationDto.SiteId.Value != 0)
                {
                    var siteExists = await db.RVSites.AnyAsync(s => s.Id == updatedReservationDto.SiteId.Value);
                    if (!siteExists)
                    {
                        return Results.BadRequest("Invalid SiteId reference.");
                    }
                    reservation.SiteId = updatedReservationDto.SiteId.Value;
                }

                if (!string.IsNullOrEmpty(updatedReservationDto.StartDate) && updatedReservationDto.StartDate != "string")
                {
                    reservation.StartDate = updatedReservationDto.StartDate;
                }

                if (!string.IsNullOrEmpty(updatedReservationDto.EndDate) && updatedReservationDto.EndDate != "string")
                {
                    reservation.EndDate = updatedReservationDto.EndDate;
                }

                if (updatedReservationDto.NumberOfGuests.HasValue && updatedReservationDto.NumberOfGuests.Value > 0)
                {
                    reservation.NumberOfGuests = updatedReservationDto.NumberOfGuests.Value;
                }

                if (updatedReservationDto.NumberOfDogs.HasValue && updatedReservationDto.NumberOfDogs.Value > 0)
                {
                    reservation.NumberOfDogs = updatedReservationDto.NumberOfDogs.Value;
                }

                if (updatedReservationDto.Status.HasValue)
                {
                    reservation.Status = updatedReservationDto.Status.Value;
                }

                await db.SaveChangesAsync();

                var updatedReservationDtoResult = new ReservationDTO
                {
                    Id = reservation.Id,
                    UserId = reservation.UserId,
                    SiteId = reservation.SiteId,
                    GuestId = reservation.GuestId,
                    StartDate = reservation.StartDate,
                    EndDate = reservation.EndDate,
                    NumberOfGuests = reservation.NumberOfGuests,
                    NumberOfDogs = reservation.NumberOfDogs,
                    Status = reservation.Status
                };

                return Results.Ok(updatedReservationDtoResult);
            });


            app.MapDelete("/reservations/{reservationId}", (OrangeLandDbContext db, int reservationId) =>
            {
                var reservation = db.Reservations
                    .Include(r => r.User)
                    .Include(r => r.Site)
                    .Include(r => r.Guest)
                    .FirstOrDefault(r => r.Id == reservationId);

                if (reservation == null)
                {
                    return Results.NotFound("Reservation not found.");
                }

                db.Reservations.Remove(reservation);
                db.SaveChanges();

                return Results.Ok("Reservation deleted successfully.");
            });


            // Add bike to reservation
            app.MapPost("/reservations/{reservationId}/bikes", async (OrangeLandDbContext db, int reservationId, BikeRentalDTO bikeRentalDto) =>
            {
                var reservationExists = await db.Reservations.AnyAsync(r => r.Id == reservationId);
                if (!reservationExists)
                {
                    return Results.NotFound("Reservation not found.");
                }

                var bike = await db.Bikes.FirstOrDefaultAsync(b => b.Id == bikeRentalDto.BikeId);
                if (bike == null)
                {
                    return Results.BadRequest("Bike not found.");
                }

                var bikeIsRented = await db.BikeRentals
                    .Include(br => br.Reservation)
                    .AnyAsync(br => br.BikeId == bikeRentalDto.BikeId && (br.Reservation.Status == ReservationStatus.Pending || br.Reservation.Status == ReservationStatus.Confirmed));

                if (bikeIsRented)
                {
                    return Results.BadRequest("Bike is already rented.");
                }

                var bikeRental = new BikeRentals
                {
                    ReservationId = reservationId,
                    BikeId = bikeRentalDto.BikeId
                };

                db.BikeRentals.Add(bikeRental);

                bike.IsAvailable = false;

                await db.SaveChangesAsync();

                var response = new
                {
                    ReservationId = bikeRental.ReservationId,
                    BikeId = bikeRental.BikeId
                };

                return Results.Created($"/reservations/{reservationId}/bikes/{bikeRentalDto.BikeId}", response);
            });


            // Get bikes for a reservation
            app.MapGet("/reservations/{reservationId}/bikes", async (OrangeLandDbContext db, int reservationId) =>
            {
                var bikeRentals = await db.BikeRentals
                    .Where(br => br.ReservationId == reservationId)
                    .Include(br => br.Bike) 
                    .Select(br => new
                    {
                        br.Bike.Id,
                        br.Bike.Type,
                        br.Bike.RentalFee,
                        br.Bike.IsAvailable
                    })
                    .ToListAsync();

                if (!bikeRentals.Any())
                {
                    return Results.NotFound("No bikes found for this reservation.");
                }

                return Results.Ok(bikeRentals);
            });

            // Remove bike from reservation
            app.MapDelete("/reservations/{reservationId}/bikes/{bikeId}", async (OrangeLandDbContext db, int reservationId, int bikeId) =>
            {
                var bikeRental = await db.BikeRentals
                    .FirstOrDefaultAsync(br => br.ReservationId == reservationId && br.BikeId == bikeId);

                if (bikeRental == null)
                {
                    return Results.NotFound("Bike rental not found for this reservation.");
                }

                db.BikeRentals.Remove(bikeRental);

                var bike = await db.Bikes.FirstOrDefaultAsync(b => b.Id == bikeId);
                if (bike != null)
                {
                    bike.IsAvailable = true;
                }

                await db.SaveChangesAsync();

                return Results.Ok("Bike removed from reservation successfully.");
            });

        }
    }
}

