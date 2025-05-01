using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Travel.admin.Models;

namespace Travel.admin.Controllers
{
	public class BookingController : Controller
	{
		private readonly AppIdentityDbContext _context;

		public BookingController(AppIdentityDbContext context)
		{
			_context = context;
		}

		public IActionResult Booking()
		{
			var bookings = _context.HotelBookings
			.Include(b => b.User)
			.Include(b => b.Hotel)
			.ToList();

			return View(bookings);
		}


		public IActionResult Create()
		{
			return View();
		}
	}
}
