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
            // GET users
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
                    Role = user.Role
                };

                return Results.Ok(userDetails);
            });

            // POSTusers
            app.MapPost("/users", async (OrangeLandDbContext db, Users newUser) =>
            {
                if (string.IsNullOrWhiteSpace(newUser.Name) || string.IsNullOrWhiteSpace(newUser.Email) || string.IsNullOrWhiteSpace(newUser.Role))
                {
                    return Results.BadRequest("Name, Email, and Role are required fields.");
                }

                var existingUser = await db.Users.FirstOrDefaultAsync(u => u.Email == newUser.Email);
                if (existingUser != null)
                {
                    return Results.BadRequest("A user with this email already exists.");
                }

                db.Users.Add(newUser);
                await db.SaveChangesAsync();

                return Results.Created($"/users/{newUser.Id}", newUser);
            });
            // GET users
            app.MapGet("/users", (OrangeLandDbContext db) =>
            {
                var users = db.Users.Select(user => new
                {
                    UserId = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    Phone = user.Phone,
                    Role = user.Role
                }).ToList();

                return Results.Ok(users);
            });
            // PUT /users/{userId}
            app.MapPut("/users/{userId}", (OrangeLandDbContext db, int userId, UpdateUserDTO updatedUser) =>
            {
                var user = db.Users.Find(userId);

                if (user == null)
                {
                    return Results.NotFound("User not found.");
                }

                // Check for unique email
                if (!string.IsNullOrWhiteSpace(updatedUser.Email) && updatedUser.Email != user.Email)
                {
                    var existingUser = db.Users.FirstOrDefault(u => u.Email == updatedUser.Email);
                    if (existingUser != null)
                    {
                        return Results.BadRequest("A user with this email already exists.");
                    }
                }

                // Update the user's details
                user.Name = updatedUser.Name ?? user.Name;
                user.Email = updatedUser.Email ?? user.Email;
                user.Phone = updatedUser.Phone ?? user.Phone;
                user.Role = updatedUser.Role ?? user.Role;

                db.SaveChanges();

                return Results.Ok(user);
            });
            // DELETE users
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


