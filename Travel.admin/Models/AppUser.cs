using Microsoft.AspNetCore.Identity;

namespace Travel.admin.Models
{
	public class AppUser : IdentityUser
	{
		public ICollection<HotelBooking> Bookings { get; set; }
	}
}
