﻿using Microsoft.EntityFrameworkCore;
using OrangeLand.Data;
using OrangeLand.Models;
using OrangeLand.DTO;

namespace OrangeLand.API
{
    public class ReservationsAPI
    {
        public static void Map(WebApplication app)
        {

            //create new reservation
            app.MapPost("/reservations", async (OrangeLandDbContext db, ReservationDTO newReservationDto) =>
            {
                // Check if Guest, User, and Site exist
                var guestExists = await db.Guests.AnyAsync(g => g.Id == newReservationDto.GuestId);
                var userExists = await db.Users.AnyAsync(u => u.Id == newReservationDto.UserId);
                var siteExists = await db.RVSites.AnyAsync(s => s.Id == newReservationDto.SiteId);

                if (!guestExists || !userExists || !siteExists)
                {
                    return Results.BadRequest("Invalid foreign key reference.");
                }

                // Ensure non-nullable fields have values
                if (newReservationDto.UserId == null || newReservationDto.SiteId == null || newReservationDto.GuestId == null ||
                    newReservationDto.StartDate == null || newReservationDto.EndDate == null || newReservationDto.Status == null)
                {
                    return Results.BadRequest("All fields must be provided.");
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

                // Fetch the created reservation
                var createdReservation = await db.Reservations.FindAsync(reservation.Id);

                if (createdReservation == null)
                {
                    return Results.NotFound("Reservation not found.");
                }

                // Map to DTO
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
            });
            // get all reservations
            app.MapGet("/reservations", (OrangeLandDbContext db) =>
            {
                var reservations = db.Reservations
                    .Include(r => r.User)
                    .Include(r => r.Site)
                    .Include(r => r.Guest)
                    .Select(r => new ReservationDTO
                    {
                        Id = r.Id,
                        UserId = r.UserId,
                        SiteId = r.SiteId,
                        GuestId = r.GuestId,
                        StartDate = r.StartDate,
                        EndDate = r.EndDate,
                        NumberOfGuests = r.NumberOfGuests,
                        NumberOfDogs = r.NumberOfDogs,
                        Status = r.Status
                    })
                    .ToList();

                return Results.Ok(reservations);
            });

            // get single reservation
            app.MapGet("/reservations/{id}", (OrangeLandDbContext db, int id) =>
            {
                var reservation = db.Reservations
                    .Include(r => r.User)
                    .Include(r => r.Site)
                    .Include(r => r.Guest)
                    .Where(r => r.Id == id)
                    .Select(r => new ReservationDTO
                    {
                        Id = r.Id,
                        UserId = r.UserId,
                        SiteId = r.SiteId,
                        GuestId = r.GuestId,
                        StartDate = r.StartDate,
                        EndDate = r.EndDate,
                        NumberOfGuests = r.NumberOfGuests,
                        NumberOfDogs = r.NumberOfDogs,
                        Status = r.Status
                    })
                    .FirstOrDefault();

                if (reservation == null)
                {
                    return Results.NotFound("Reservation not found.");
                }

                return Results.Ok(reservation);
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

                if (updatedReservationDto.NumberOfGuests.HasValue)
                {
                    reservation.NumberOfGuests = updatedReservationDto.NumberOfGuests.Value;
                }

                if (updatedReservationDto.NumberOfDogs.HasValue)
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


        }
    }
}
