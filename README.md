# OrangeLand Minimal API

## Overview

The **OrangeLand** application is a C# minimal API designed to manage reservations for an RV site, including users, guests, bikes, and bike rentals. It provides endpoints for creating, retrieving, updating, and deleting reservations, as well as viewing and managing bikes and users.

## Technologies

- **.NET 8.0**
- **ASP.NET Core Minimal API**
- **Entity Framework Core**
- **SQL Server**
- **Postman**

## Setup Instructions

1. **In the csharp directory of your workspace, create the web API project like this:** *dotnet new webapi -o OrangeLand -minimal*
  
2. **Inside the OrangeLand directory, run:** *dotnet new gitignore*
  
3. **Install these required dependencies with:**
   *dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL --version 8.0*  
  *dotnet add package Microsoft.EntityFrameworkCore.Design --version 8.0*

4. **Run this to be able to store secrets for this app:** *dotnet user-secrets init*
  
5. **Add the connection string to the secrets for this app with this (make sure you change <your_postgresql_password> to your db password!):** *dotnet user-secrets set "OrangeLandDbConnectionString" "Host=localhost;Port=5432;Username=postgres;Password=<your_postgresql_password>;Database=OrangeLand"*





## Database Schema

### Users

- **UserId** (Primary Key)
- **Name**
- **Email**
- **Phone**
- **Role**

### RVSites

- **SiteId** (Primary Key)
- **SiteNumber**
- **HasGrassyArea** (Boolean)
- **IsPullThrough** (Boolean)

### Reservations

- **ReservationId** (Primary Key)
- **UserId** (Foreign Key referencing Users.UserId)
- **SiteId** (Foreign Key referencing RVSites.SiteId)
- **GuestId** (Foreign Key referencing Guests.GuestId)
- **StartDate**
- **EndDate**
- **NumberOfGuests**
- **NumberOfDogs**
- **Status** (Enum)

### Guests

- **GuestId** (Primary Key)
- **Name**
- **RVType**

### Bikes

- **BikeId** (Primary Key)
- **Type**
- **RentalFee** (Decimal)
- **IsAvailable** (Boolean)

### BikeRentals

- **ReservationId** (Foreign Key referencing Reservations.ReservationId, Primary Key)
- **BikeId** (Foreign Key referencing Bikes.BikeId, Primary Key)

## Endpoints

### Reservations

- **GET /reservations**: Retrieve all reservations.
- **GET /reservations/{reservationId}**: Retrieve a specific reservation by ID.
- **POST /reservations**: Create a new reservation.
- **PUT /reservations/{reservationId}**: Update an existing reservation.
- **DELETE /reservations/{reservationId}**: Delete a reservation.

### Bikes

- **GET /bikes**: Retrieve all bikes.
- **GET /bikes/{bikeId}**: Retrieve a specific bike by ID.

### Users

- **GET /users**: Retrieve all users.
- **GET /users/{userId}**: Retrieve a specific user by ID.
- **POST /users**: Create a new user.
- **PUT /users/{userId}**: Update an existing user.
- **DELETE /users/{userId}**: Delete a user.

### Guests

- **GET /guests**: Retrieve all guests.
- **GET /guests/{guestId}**: Retrieve a specific guest by ID.
- **POST /guests**: Create a new guest.
- **PUT /guests/{guestId}**: Update an existing guest.
- **DELETE /guests/{guestId}**: Delete a guest.

### RVSites

- **GET /rvsites**: Retrieve all RV sites.
- **GET /rvsites/{siteId}**: Retrieve a specific RV site by ID.

## Data Transfer Objects (DTOs)

- **ReservationDTO**
- **BikesDTO**
- **UserDTO**
- **GuestDTO**
- **RVSiteDTO**
- **CreateUserDTO**
- **CreateGuestDTO**
- **UpdateUserDTO**
- **UpdateGuestDTO**
