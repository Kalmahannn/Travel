using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging; // Логгерді қосу
using TravelistaMVC.Models;
using TravelistaMVC.Services;
using TravelistaMVC.ViewModels;
using TravelistaMVC.Filters; // Фильтрдің жолын қосу


using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TravelistaMVC.Data;
using TravelistaMVC.Filters;
using System.Linq;

namespace TravelistaMVC.Controllers
{
    [ServiceFilter(typeof(LoggingActionFilter))]
    [ServiceFilter(typeof(CustomAuthorizationFilter))]
    [ServiceFilter(typeof(CustomResultFilter))]
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(AppDbContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.LogInformation("Index беті ашылды.");
            return View();
        }

        public IActionResult About1()
        {
            _logger.LogInformation("About1 беті ашылды.");
            return View();
        }

        public IActionResult Packages()
        {
            _logger.LogInformation("Packages беті ашылды.");

            var packages = new List<TourPackage>
            {
                new TourPackage {
                    Id = 2,
                    Name = "Holiday Sea Beach",
                    Destination = "Maldives",
                    Price = 350,
                    ImageUrl = "/img/packages/d1.jpg"
                },
                new TourPackage {
                    Id = 3,
                    Name = "Mountain Adventure",
                    Destination = "Switzerland",
                    Price = 400,
                    ImageUrl = "/img/packages/d2.jpg"
                },
                new TourPackage {
                    Id = 4,
                    Name = "Safari Expedition",
                    Destination = "Kenya",
                    Price = 450,
                    ImageUrl = "/img/packages/d3.jpg"
                },
                new TourPackage {
                    Id = 5,
                    Name = "Cultural Tour",
                    Destination = "Japan",
                    Price = 300,
                    ImageUrl = "/img/packages/d4.jpg"
                },
                new TourPackage {
                    Id = 6,
                    Name = "Island Getaway",
                    Destination = "Bahamas",
                    Price = 500,
                    ImageUrl = "/img/packages/d5.jpg"
                },
                new TourPackage {
                    Id = 7,
                    Name = "Historical Europe",
                    Destination = "Rome, Italy",
                    Price = 380,
                    ImageUrl = "/img/packages/d6.jpg"
                },
            };

            return View(packages);
        }

        public IActionResult Hotels()
        {
            _logger.LogInformation("Hotels беті ашылды.");
            var hotels1 = _context.Hotels.ToList();
            var hotels = new List<Hotel>
      
            {
                new Hotel
                {
                    Id = 1,
                    Name = "Hilton Star Hotel",
                    Location = "United States of America",
                    PricePerNight = 250,
                    ImageUrl = "/img/hotels/d1.jpg",
                    Stars = 4,
                    AirCondition = true,
                    Restaurant = true,
                    RoomService = false,
                    Wifi = true,
                    SwimmingPool = true,
                    Gymnasium = false
                },
                new Hotel
                {
                    Id = 2,
                    Name = "Mountain View Resort",
                    Location = "Switzerland",
                    PricePerNight = 300,
                    ImageUrl = "/img/hotels/d2.jpg",
                    Stars = 5,
                    AirCondition = true,
                    Restaurant = true,
                    RoomService = true,
                    Wifi = true,
                    SwimmingPool = true,
                    Gymnasium = true
                },
                new Hotel
                {
                    Id = 3,
                    Name = "Desert Oasis Hotel",
                    Location = "Morocco",
                    PricePerNight = 220,
                    ImageUrl = "/img/hotels/d3.jpg",
                    Stars = 3,
                    AirCondition = true,
                    Restaurant = false,
                    RoomService = true,
                    Wifi = true,
                    SwimmingPool = false,
                    Gymnasium = false
                },
                new Hotel
                {
                    Id = 4,
                    Name = "Island Paradise",
                    Location = "Bahamas",
                    PricePerNight = 400,
                    ImageUrl = "/img/hotels/d4.jpg",
                    Stars = 5,
                    AirCondition = true,
                    Restaurant = true,
                    RoomService = true,
                    Wifi = true,
                    SwimmingPool = true,
                    Gymnasium = true
                },
                new Hotel
                {
                    Id = 5,
                    Name = "Urban Central Hotel",
                    Location = "Tokyo, Japan",
                    PricePerNight = 280,
                    ImageUrl = "/img/hotels/d5.jpg",
                    Stars = 4,
                    AirCondition = true,
                    Restaurant = true,
                    RoomService = true,
                    Wifi = true,
                    SwimmingPool = false,
                    Gymnasium = true
                },
                new Hotel
                {
                    Id = 6,
                    Name = "Lakeview Retreat",
                    Location = "Canada",
                    PricePerNight = 260,
                    ImageUrl = "/img/hotels/d6.jpg",
                    Stars = 4,
                    AirCondition = true,
                    Restaurant = true,
                    RoomService = true,
                    Wifi = true,
                    SwimmingPool = true,
                    Gymnasium = false
                }
            };

            return View(hotels);
        }

        public IActionResult Insurance()
        {
            _logger.LogInformation("Insurance беті ашылды.");
            return View();
        }

        public IActionResult Elements()
        {
            _logger.LogInformation("Elements беті ашылды.");
            return View();
        }

        public IActionResult Contact()
        {
            _logger.LogInformation("Contact беті ашылды.");
            return View();
        }

        [HttpGet]
        public IActionResult BookHotel()
        {
            _logger.LogInformation("BookHotel GET беті ашылды.");
            return View();
        }

        [HttpPost]
        public IActionResult SetLanguage(string culture)// тіл ауыстыру
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return Redirect(Request.Headers["Referer"].ToString());
        }


        [HttpPost]
        public async Task<IActionResult> BookHotel(HotelBooking model)
        {
            if (ModelState.IsValid)
            {
                _logger.LogInformation("BookHotel POST: жаңа брондау жасалды.");

                var emailService = new EmailService();
                var body = $@"
                <h2>New Hotel Booking</h2>
                <p><strong>Name:</strong> {model.Name}</p>
                <p><strong>Email:</strong> {model.Email}</p>
                <p><strong>Check-In:</strong> {model.CheckInDate.ToShortDateString()}</p>
                <p><strong>Check-Out:</strong> {model.CheckOutDate.ToShortDateString()}</p>";

                await emailService.SendBookingEmail("kalmahanesjan@gmail.com", "New Hotel Booking", body);

                TempData["Message"] = "Брондау сәтті өтті!";
                return RedirectToAction("BookHotel");
            }

            _logger.LogWarning("BookHotel POST: ModelState жарамсыз.");
            return View(model);
        }
    }
}
