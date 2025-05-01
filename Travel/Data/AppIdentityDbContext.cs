using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Travel.Controllers;
using Travel.Models;
using TravelistaMVC.Models;

namespace Travel.Data
{
	public class AppIdentityDbContext : IdentityDbContext<AppUser>
	{
		public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options) { }


		public DbSet<Hotel> Hotels { get; set; }
		public DbSet<HotelBooking> HotelBookings { get; set; }
		public DbSet<TourPackage> TourPackages { get; set; }
		public DbSet<BlogPost> BlogPosts { get; set; }
		public DbSet<Category> Categories { get; set; }


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<HotelBooking>()
				.HasOne(b => b.User)
				.WithMany(u => u.Bookings )
				.HasForeignKey(b => b.UserId);

			modelBuilder.Entity<BlogPost>()
				.HasOne(b => b.Category)
				.WithMany(c => c.BlogPosts)
				.HasForeignKey(b => b.CategoryId)
				.OnDelete(DeleteBehavior.Restrict); 


		}


	}
	}
