﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using OrangeLand.Data;

#nullable disable

namespace OrangeLand.Migrations
{
    [DbContext(typeof(OrangeLandDbContext))]
    [Migration("20240522055356_UpdatedModels")]
    partial class UpdatedModels
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("OrangeLand.Models.BikeRentals", b =>
                {
                    b.Property<int>("ReservationId")
                        .HasColumnType("integer");

                    b.Property<int>("BikeId")
                        .HasColumnType("integer");

                    b.HasKey("ReservationId", "BikeId");

                    b.HasIndex("BikeId");

                    b.ToTable("BikeRentals");

                    b.HasData(
                        new
                        {
                            ReservationId = 1,
                            BikeId = 1
                        },
                        new
                        {
                            ReservationId = 2,
                            BikeId = 2
                        },
                        new
                        {
                            ReservationId = 3,
                            BikeId = 3
                        },
                        new
                        {
                            ReservationId = 4,
                            BikeId = 4
                        });
                });

            modelBuilder.Entity("OrangeLand.Models.Bikes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("boolean");

                    b.Property<decimal>("RentalFee")
                        .HasColumnType("numeric");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Bikes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            IsAvailable = true,
                            RentalFee = 15.00m,
                            Type = "Mountain Bike"
                        },
                        new
                        {
                            Id = 2,
                            IsAvailable = true,
                            RentalFee = 12.00m,
                            Type = "Road Bike"
                        },
                        new
                        {
                            Id = 3,
                            IsAvailable = true,
                            RentalFee = 10.00m,
                            Type = "Hybrid Bike"
                        },
                        new
                        {
                            Id = 4,
                            IsAvailable = true,
                            RentalFee = 20.00m,
                            Type = "Electric Bike"
                        });
                });

            modelBuilder.Entity("OrangeLand.Models.Guests", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("PreferredSiteId")
                        .HasColumnType("integer");

                    b.Property<string>("RVType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Guests");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Amuro Ray",
                            PreferredSiteId = 1,
                            RVType = "Mobile Suit"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Char Aznable",
                            PreferredSiteId = 2,
                            RVType = "Mobile Suit"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Kamille Bidan",
                            PreferredSiteId = 3,
                            RVType = "Mobile Suit"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Haman Karn",
                            PreferredSiteId = 4,
                            RVType = "Mobile Suit"
                        });
                });

            modelBuilder.Entity("OrangeLand.Models.RVSites", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("HasGrassyArea")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsPullThrough")
                        .HasColumnType("boolean");

                    b.Property<string>("SiteNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("RVSites");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            HasGrassyArea = true,
                            IsPullThrough = false,
                            SiteNumber = "A1"
                        },
                        new
                        {
                            Id = 2,
                            HasGrassyArea = false,
                            IsPullThrough = true,
                            SiteNumber = "A2"
                        },
                        new
                        {
                            Id = 3,
                            HasGrassyArea = true,
                            IsPullThrough = false,
                            SiteNumber = "B1"
                        },
                        new
                        {
                            Id = 4,
                            HasGrassyArea = false,
                            IsPullThrough = true,
                            SiteNumber = "B2"
                        },
                        new
                        {
                            Id = 5,
                            HasGrassyArea = true,
                            IsPullThrough = false,
                            SiteNumber = "C1"
                        },
                        new
                        {
                            Id = 6,
                            HasGrassyArea = false,
                            IsPullThrough = true,
                            SiteNumber = "C2"
                        },
                        new
                        {
                            Id = 7,
                            HasGrassyArea = true,
                            IsPullThrough = false,
                            SiteNumber = "D1"
                        },
                        new
                        {
                            Id = 8,
                            HasGrassyArea = false,
                            IsPullThrough = true,
                            SiteNumber = "D2"
                        },
                        new
                        {
                            Id = 9,
                            HasGrassyArea = true,
                            IsPullThrough = false,
                            SiteNumber = "E1"
                        },
                        new
                        {
                            Id = 10,
                            HasGrassyArea = false,
                            IsPullThrough = true,
                            SiteNumber = "E2"
                        });
                });

            modelBuilder.Entity("OrangeLand.Models.Reservations", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("EndDate")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("GuestId")
                        .HasColumnType("integer");

                    b.Property<int>("NumberOfDogs")
                        .HasColumnType("integer");

                    b.Property<int>("NumberOfGuests")
                        .HasColumnType("integer");

                    b.Property<int>("SiteId")
                        .HasColumnType("integer");

                    b.Property<string>("StartDate")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("GuestId");

                    b.HasIndex("SiteId");

                    b.HasIndex("UserId");

                    b.ToTable("Reservations");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            EndDate = "2024-06-10",
                            GuestId = 1,
                            NumberOfDogs = 0,
                            NumberOfGuests = 1,
                            SiteId = 1,
                            StartDate = "2024-06-01",
                            Status = 1,
                            UserId = 2
                        },
                        new
                        {
                            Id = 2,
                            EndDate = "2024-06-11",
                            GuestId = 2,
                            NumberOfDogs = 0,
                            NumberOfGuests = 1,
                            SiteId = 2,
                            StartDate = "2024-06-02",
                            Status = 1,
                            UserId = 3
                        },
                        new
                        {
                            Id = 3,
                            EndDate = "2024-06-12",
                            GuestId = 3,
                            NumberOfDogs = 0,
                            NumberOfGuests = 1,
                            SiteId = 3,
                            StartDate = "2024-06-03",
                            Status = 1,
                            UserId = 4
                        },
                        new
                        {
                            Id = 4,
                            EndDate = "2024-06-13",
                            GuestId = 4,
                            NumberOfDogs = 0,
                            NumberOfGuests = 1,
                            SiteId = 4,
                            StartDate = "2024-06-04",
                            Status = 1,
                            UserId = 2
                        });
                });

            modelBuilder.Entity("OrangeLand.Models.Users", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "manager@orangland.com",
                            IsAdmin = true,
                            Name = "Manager",
                            Phone = "123-456-7890"
                        },
                        new
                        {
                            Id = 2,
                            Email = "employee1@orangland.com",
                            IsAdmin = false,
                            Name = "Employee1",
                            Phone = "123-456-7891"
                        },
                        new
                        {
                            Id = 3,
                            Email = "employee2@orangland.com",
                            IsAdmin = false,
                            Name = "Employee2",
                            Phone = "123-456-7892"
                        },
                        new
                        {
                            Id = 4,
                            Email = "employee3@orangland.com",
                            IsAdmin = false,
                            Name = "Employee3",
                            Phone = "123-456-7893"
                        });
                });

            modelBuilder.Entity("OrangeLand.Models.BikeRentals", b =>
                {
                    b.HasOne("OrangeLand.Models.Bikes", "Bike")
                        .WithMany("BikeRentals")
                        .HasForeignKey("BikeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OrangeLand.Models.Reservations", "Reservation")
                        .WithMany("BikeRentals")
                        .HasForeignKey("ReservationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bike");

                    b.Navigation("Reservation");
                });

            modelBuilder.Entity("OrangeLand.Models.Reservations", b =>
                {
                    b.HasOne("OrangeLand.Models.Guests", "Guest")
                        .WithMany("Reservations")
                        .HasForeignKey("GuestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OrangeLand.Models.RVSites", "Site")
                        .WithMany("Reservations")
                        .HasForeignKey("SiteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OrangeLand.Models.Users", "User")
                        .WithMany("Reservations")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Guest");

                    b.Navigation("Site");

                    b.Navigation("User");
                });

            modelBuilder.Entity("OrangeLand.Models.Bikes", b =>
                {
                    b.Navigation("BikeRentals");
                });

            modelBuilder.Entity("OrangeLand.Models.Guests", b =>
                {
                    b.Navigation("Reservations");
                });

            modelBuilder.Entity("OrangeLand.Models.RVSites", b =>
                {
                    b.Navigation("Reservations");
                });

            modelBuilder.Entity("OrangeLand.Models.Reservations", b =>
                {
                    b.Navigation("BikeRentals");
                });

            modelBuilder.Entity("OrangeLand.Models.Users", b =>
                {
                    b.Navigation("Reservations");
                });
#pragma warning restore 612, 618
        }
    }
}
