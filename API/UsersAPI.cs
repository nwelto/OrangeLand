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

            // POST users
            app.MapPost("/users", (OrangeLandDbContext db, CreateUserDTO newUserDto) =>
            {
                if (string.IsNullOrWhiteSpace(newUserDto.Name) || string.IsNullOrWhiteSpace(newUserDto.Email) || !newUserDto.IsValidRole())
                {
                    return Results.BadRequest("Name, Email, and valid Role are required fields.");
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
                    Role = newUserDto.Role
                };

                db.Users.Add(newUser);
                db.SaveChanges();

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

            // PUT users
            app.MapPut("/users/{userId}", (OrangeLandDbContext db, int userId, UpdateUserDTO updatedUser) =>
            {
                var user = db.Users.Find(userId);

                if (user == null)
                {
                    return Results.NotFound("User not found.");
                }

                if (!string.IsNullOrWhiteSpace(updatedUser.Role) && !updatedUser.IsValidRole())
                {
                    return Results.BadRequest("Role must be either 'Manager' or 'Employee'.");
                }

                if (!string.IsNullOrWhiteSpace(updatedUser.Email) && updatedUser.Email != user.Email)
                {
                    var existingUser = db.Users.FirstOrDefault(u => u.Email == updatedUser.Email);
                    if (existingUser != null)
                    {
                        return Results.BadRequest("A user with this email already exists.");
                    }
                }

              
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


