using Microsoft.EntityFrameworkCore;
using OrangeLand.Data;
using OrangeLand.Models;
using OrangeLand.DTO;

namespace OrangeLand.API
{
    public class UsersAPI
    {
        public static void Map(WebApplication app)
        {
            // Get user by ID
            app.MapGet("/users/{userId}", (OrangeLandDbContext db, int userId) =>
            {
                var user = db.Users.Find(userId);

                if (user == null)
                {
                    return Results.NotFound("User not found.");
                }

                var userDetails = new
                {
                    UserId = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    Phone = user.Phone,
                    IsAdmin = user.IsAdmin
                };

                return Results.Ok(userDetails);
            });

            // Create user
            app.MapPost("/users", (OrangeLandDbContext db, CreateUserDTO newUserDto) =>
            {
                if (string.IsNullOrWhiteSpace(newUserDto.Name) || string.IsNullOrWhiteSpace(newUserDto.Email))
                {
                    return Results.BadRequest("Name and Email are required fields.");
                }

                var existingUser = db.Users.FirstOrDefault(u => u.Email == newUserDto.Email);
                if (existingUser != null)
                {
                    return Results.BadRequest("A user with this email already exists.");
                }

                var newUser = new Users
                {
                    Name = newUserDto.Name,
                    Email = newUserDto.Email,
                    Phone = newUserDto.Phone,
                    IsAdmin = false 
                };

                db.Users.Add(newUser);
                db.SaveChanges();

                return Results.Created($"/users/{newUser.Id}", newUser);
            });

            // Get all users
            app.MapGet("/users", (OrangeLandDbContext db) =>
            {
                var users = db.Users.Select(user => new
                {
                    UserId = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    Phone = user.Phone,
                    IsAdmin = user.IsAdmin
                }).ToList();

                return Results.Ok(users);
            });

            // Update user
            app.MapPut("/users/{userId}", (OrangeLandDbContext db, int userId, UpdateUserDTO updatedUserDto) =>
            {
                var user = db.Users.Find(userId);

                if (user == null)
                {
                    return Results.NotFound("User not found.");
                }

                // Check for unique email
                if (!string.IsNullOrWhiteSpace(updatedUserDto.Email) && updatedUserDto.Email != user.Email)
                {
                    var existingUser = db.Users.FirstOrDefault(u => u.Email == updatedUserDto.Email);
                    if (existingUser != null)
                    {
                        return Results.BadRequest("A user with this email already exists.");
                    }
                }

                // Update the user's details
                user.Name = updatedUserDto.Name ?? user.Name;
                user.Email = updatedUserDto.Email ?? user.Email;
                user.Phone = updatedUserDto.Phone ?? user.Phone;

                db.SaveChanges();

                return Results.Ok(user);
            });

            // Delete user
            app.MapDelete("/users/{userId}", (OrangeLandDbContext db, int userId) =>
            {
                var user = db.Users.Find(userId);

                if (user == null)
                {
                    return Results.NotFound("User not found.");
                }

                db.Users.Remove(user);
                db.SaveChanges();

                return Results.Ok("User successfully deleted.");
            });
        }
    }
}




