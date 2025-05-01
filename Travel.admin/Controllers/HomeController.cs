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
