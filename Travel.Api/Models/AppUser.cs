using Microsoft.AspNetCore.Identity;

namespace Travel.Api.Models
{
	public class AppUser : IdentityUser
	{
		public ICollection<HotelBooking> Bookings { get; set; }
	}
}
