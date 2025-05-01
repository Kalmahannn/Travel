using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using Travel.Data;


public class BookingController : Controller
{
    private readonly ILogger<BookingController> _logger;
    private readonly AppIdentityDbContext _context;
    private readonly UserManager<AppUser> _userManager;


	public BookingController(ILogger<BookingController> logger,AppIdentityDbContext context, UserManager<AppUser> userManager)
	{
		_logger = logger;
		_context = context;
		_userManager = userManager;
	}




	public IActionResult MyBrons()
	{
		var userId = _userManager.GetUserId(User);

		var bookings = _context.HotelBookings
			.Include(b => b.Hotel)
			.Where(b => b.UserId == userId)
			.Select(b => new
			{
				b.Id,
				HotelName = b.Hotel.Name,
				b.CheckInDate,
				b.CheckOutDate
			})
			.ToList();

		return View(bookings);
	}









	public IActionResult Index()
    {
        _logger.LogInformation("Қолданушы {User} бронирование бетіне кірді.", User.Identity?.Name ?? "Анықталмаған");
        return View();
    }

    [HttpPost]
    public IActionResult Book(string destination, DateTime date)
    {
        _logger.LogInformation("Пайдаланушы {User} {Destination} бағытына {Date} күніне брондауға тырысты.",
            User.Identity?.Name ?? "Анықталмаған", destination, date);

        try
        {
            // Брондау логикасы (мысалы, мәліметтер қорына сақтау)

            _logger.LogInformation("Брондау сәтті аяқталды.");
            return RedirectToAction("Success");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Брондау кезінде қате пайда болды");
            return RedirectToAction("Error");
        }
    }
}
