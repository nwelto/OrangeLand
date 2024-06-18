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

            // Check User
            app.MapGet("/checkUser/{uid}", async (OrangeLandDbContext db, string uid) =>
            {
                var user = await db.Users.Where(u => u.Uid == uid).FirstOrDefaultAsync();

                if (user == null)
                {
                    return Results.NotFound();
                }

                var userInfo = new
                {
                    user.Id,
                    user.Name,
                    user.Email,
                    user.IsAdmin,
                    user.Uid
                };

                return Results.Ok(userInfo);
            });


            app.MapPost("/user", (OrangeLandDbContext db, Users newUser) =>
            {
                if (string.IsNullOrEmpty(newUser.Uid) || string.IsNullOrEmpty(newUser.Email) || string.IsNullOrEmpty(newUser.Name))
                {
                    return Results.BadRequest("Invalid user data.");
                }

                db.Users.Add(newUser);
                db.SaveChanges();
                return Results.Created($"/user/{newUser.Id}", newUser);
            });

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
                    IsAdmin = user.IsAdmin
                };

                return Results.Ok(userDetails);
            });


            // Get all users
            app.MapGet("/users", (OrangeLandDbContext db) =>
            {
                var users = db.Users.Select(user => new
                {
                    UserId = user.Id,
                    Name = user.Name,
                    Email = user.Email,
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




