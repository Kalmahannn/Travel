using System.Collections.Generic;
using TravelistaMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace TravelistaMVC.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

		public DbSet<Hotel> Hotels { get; set; }
		public DbSet<HotelBooking> HotelBookings { get; set; }
        public DbSet<TourPackage> TourPackages { get; set; }
	}
}
