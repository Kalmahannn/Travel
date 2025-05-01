using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging; 
using TravelistaMVC.Models;
using TravelistaMVC.Services;
using TravelistaMVC.ViewModels;
using TravelistaMVC.Filters; 


using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using TravelistaMVC.Filters;
using System.Linq;
using System.Security.Claims;
using Travel.Models;
using Travel.Data;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Numerics;
using Travel.AppFilter;

namespace TravelistaMVC.Controllers
{


    [ServiceFilter(typeof(LoggingActionFilter))]
    [ServiceFilter(typeof(CustomAuthorizationFilter))]
    [ServiceFilter(typeof(CustomResultFilter))]
    public class HomeController : Controller
    {
        private readonly AppIdentityDbContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(AppIdentityDbContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }


        public IActionResult Index()
        {
            _logger.LogInformation("Index беті ашылды.");
            return View();
        }

		[IEFilter]
		public IActionResult About1()
        {
            _logger.LogInformation("About1 беті ашылды.");
            return View();
        }



        //Пакеты
        public async Task<IActionResult> Packages()
        {
            _logger.LogInformation("Packages беті ашылды.");

            var packages = _context.TourPackages.ToList();


            return View(packages);
        }




		public async Task<IActionResult> GetImageAsync(int id)
		{
			var hotel = new Hotel();


			using (var client = new HttpClient())
			{
				var token = Request.Cookies["token"];

				client.DefaultRequestHeaders.Authorization =
					new AuthenticationHeaderValue("Bearer", token);

				using (var responce = await client.GetAsync($"http://localhost:5101/api/Hotel/GetById/{id}"))
				{
					var result = await responce.Content.ReadAsStringAsync();
					hotel = JsonConvert.DeserializeObject<Hotel>(result);
				}

			}



			if (hotel == null || hotel.ImageData == null)
			{
				return NotFound();
			}

			return File(hotel.ImageData, "image/jpeg"); // Можно также использовать другие форматы
		}



        //Отели
		public async Task<IActionResult> HotelsAsync()
        {
            _logger.LogInformation("Hotels беті ашылды.");
            var hotels = new List<Hotel>();


			using (var client = new HttpClient())
			{
				var token = Request.Cookies["token"];

				client.DefaultRequestHeaders.Authorization =
					new AuthenticationHeaderValue("Bearer", token);

				using (var responce = await client.GetAsync("http://localhost:5101/api/Hotel"))
				{
					var result = await responce.Content.ReadAsStringAsync();
					hotels = JsonConvert.DeserializeObject<List<Hotel>>(result);
				}

			}


			return View(hotels);
        }




        public  IActionResult CreateHotel()
        {
            return View();
        }



		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> CreateHotel(Hotel hotel,IFormFile ImageFile)
		{
			if (ModelState.IsValid)
			{

				if (ImageFile != null && ImageFile.Length > 0)
				{
					
					using (var memoryStream = new MemoryStream())
					{
						await ImageFile.CopyToAsync(memoryStream);
                        hotel.ImageData = memoryStream.ToArray();
					}
				}


				_context.Hotels.Add(hotel);
				_context.SaveChanges();
				return RedirectToAction("Hotels","Home"); 
			}

			return View(hotel); 
		}




		[HttpGet]
		public async Task<IActionResult> BookHotel(int hotelId)
		{
			_logger.LogInformation("BookHotel GET беті ашылды.");
			var _hotel = new Hotel();
			

			using (var client = new HttpClient())
			{
				var token = Request.Cookies["token"];

				client.DefaultRequestHeaders.Authorization =
					new AuthenticationHeaderValue("Bearer", token);

				using (var responce = await client.GetAsync($"http://localhost:5101/api/Hotel/GetById/{hotelId}"))
				{
					var result = await responce.Content.ReadAsStringAsync();
					_hotel = JsonConvert.DeserializeObject<Hotel>(result);
				}

			}

			var model = new HotelBooking { HotelId = hotelId };



			return View(model);
		}



		[HttpPost]
		public async Task<IActionResult> BookHotel(HotelBooking model)
		{
			if (ModelState.IsValid)
			{

				if (!ModelState.IsValid)
					return View(model);

				var hotel = await _context.Hotels.FindAsync(model.HotelId);
				if (hotel == null)
				{
					ModelState.AddModelError("", "Отель не найден.");
					return View(model);
				}


				var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

				if (string.IsNullOrEmpty(userId))
				{
					ModelState.AddModelError("", "Пользователь не авторизован.");
					return View(model);
				}

				model.UserId = userId;

				_context.HotelBookings.Add(model);
				await _context.SaveChangesAsync();

				return RedirectToAction("BookingConfirmation");

			}

			_logger.LogWarning("BookHotel POST: ModelState жарамсыз.");
			return View(model);
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


       
    }
}
