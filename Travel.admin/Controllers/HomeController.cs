using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Travel.admin.Models;

namespace Travel.admin.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly AppIdentityDbContext _context;

		public HomeController(ILogger<HomeController> logger,AppIdentityDbContext context)
		{
			_logger = logger;
			_context = context;
		}

		public IActionResult Index()
		{
			var hotels = _context.Hotels.ToList();
			return View(hotels);
		}

		
		public IActionResult CreateHotel()
		{
			return View();
		}



		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> CreateHotel(Hotel hotel, IFormFile ImageFile)
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
				return RedirectToAction("Index", "Home");
			}

			return View(hotel);
		}



		[HttpGet]
		public async Task<IActionResult> EditHotel(int id)
		{
			var hotel = await _context.Hotels.FindAsync(id);
			if (hotel == null)
				return NotFound();

			return View(hotel);
		}


		[HttpPost]
		public async Task<IActionResult> EditHotel(int id, Hotel updatedHotel, IFormFile? ImageFile)
		{
			var hotel = await _context.Hotels.FindAsync(id);
			if (hotel == null)
				return NotFound();

			hotel.Name = updatedHotel.Name;
			hotel.Location = updatedHotel.Location;
			hotel.PricePerNight = updatedHotel.PricePerNight;
			hotel.Stars = updatedHotel.Stars;
			hotel.SwimmingPool = updatedHotel.SwimmingPool;
			hotel.Gymnasium = updatedHotel.Gymnasium;
			hotel.Wifi = updatedHotel.Wifi;
			hotel.RoomService = updatedHotel.RoomService;
			hotel.AirCondition = updatedHotel.AirCondition;
			hotel.Restaurant = updatedHotel.Restaurant;

			if (ImageFile != null && ImageFile.Length > 0)
			{
				using (var ms = new MemoryStream())
				{
					await ImageFile.CopyToAsync(ms);
					hotel.ImageData = ms.ToArray();
				}
			}

			await _context.SaveChangesAsync();
			return RedirectToAction("Index","Home"); 
		}


		[HttpGet]
		public async Task<IActionResult> DeleteHotel(int id)
		{
			var hotel = await _context.Hotels.FindAsync(id);
			if (hotel == null)
				return NotFound();

			_context.Hotels.Remove(hotel);
			await _context.SaveChangesAsync();

			return RedirectToAction("Index","Home"); 
		}








		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
