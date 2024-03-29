﻿// <auto-generated />
using System;
using BookingApplication;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BookingApplication.Migrations
{
    [DbContext(typeof(BookingAppContext))]
    partial class BookingAppContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.1");

            modelBuilder.Entity("BookingApplication.Accomodation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AccomodationType")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("NoOfRooms")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Accomodations");
                });

            modelBuilder.Entity("BookingApplication.Booking", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AccomodationId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AccomodationId");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("BookingApplication.Booking", b =>
                {
                    b.HasOne("BookingApplication.Accomodation", "Accomodations")
                        .WithMany("Bookings")
                        .HasForeignKey("AccomodationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Accomodations");
                });

            modelBuilder.Entity("BookingApplication.Accomodation", b =>
                {
                    b.Navigation("Bookings");
                });
#pragma warning restore 612, 618
        }
    }
}
