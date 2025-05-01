using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Travel.Api.Models
{
	public class AppIdentityDbContext : IdentityDbContext<AppUser>
	{
		public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options) { }


		public DbSet<Hotel> Hotels { get; set; }
		public DbSet<HotelBooking> HotelBookings { get; set; }
		public DbSet<TourPackage> TourPackages { get; set; }


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<HotelBooking>()
				.HasOne(b => b.User)
				.WithMany(u => u.Bookings)
				.HasForeignKey(b => b.UserId);




		}
	}
}
