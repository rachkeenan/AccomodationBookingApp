using BookingApplication;
using System;
using System.Linq;
using System.Reflection.Metadata;

using var db = new BookingAppContext();

// Note: This sample requires the database to be created before running.
// Note: Only run once, otherwise data will be duplicated
Console.WriteLine($"Database path: {db.DbPath}.");

// Create accomodations
Console.WriteLine("Inserting a new Accomodation");
db.Add(new Accomodation { Name = "Sunshine Villa", AccomodationType = "Villa", NoOfRooms = 7 });
db.Add(new Accomodation { Name = "Meadow Cabin", AccomodationType = "Cabin", NoOfRooms = 3 });
db.Add(new Accomodation { Name = "Penthouse", AccomodationType = "Apartment", NoOfRooms = 3 });
db.Add(new Accomodation { Name = "Home Sweet Home", AccomodationType = "House", NoOfRooms = 4 });
db.SaveChanges();

// Create bookings
Console.WriteLine("Inserting a new Booking");
db.Add(new Booking { StartDate = DateTime.Now.AddDays(-2), EndDate = DateTime.Now.AddDays(+4), AccomodationId = 1 });
db.Add(new Booking { StartDate = DateTime.Now.AddDays(+1), EndDate = DateTime.Now.AddDays(+7), AccomodationId = 2 });
db.Add(new Booking { StartDate = DateTime.Now.AddDays(-3), EndDate = DateTime.Now.AddDays(+2), AccomodationId = 3 });
db.Add(new Booking { StartDate = DateTime.Now.AddDays(+5), EndDate = DateTime.Now.AddDays(+8), AccomodationId = 4 });
db.SaveChanges();

// Delete accomodations
//Console.WriteLine("Delete accomodations");
//db.Remove(Accomodation);
//db.SaveChanges();
