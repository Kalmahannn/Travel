using Microsoft.AspNetCore.Identity;
using TravelistaMVC.Models;

namespace Travel.Data
{
	public class AppUser : IdentityUser
	{
		public ICollection<HotelBooking> Bookings { get; set; }
	}
}
