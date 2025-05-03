using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Travel.admin.Models;

namespace Travel.admin.Controllers
{
	public class PackagesController : Controller
	{
		private readonly ILogger<PackagesController> _logger;
		private readonly AppIdentityDbContext _context;


		public PackagesController(ILogger<PackagesController> logger,AppIdentityDbContext context)
		{
			_logger = logger;
			_context = context;
		}




		public async Task<IActionResult> Package()
		{
			var packages =  _context.TourPackages.ToList();
			return View(packages);
		}




		public IActionResult CreatePackage()
		{
			return View();
		}




		[HttpPost]
		public async Task<IActionResult> CreatePackage(TourPackage package,IFormFile ImageFile)
		{
			if (ModelState.IsValid)
			{

				if (ImageFile != null && ImageFile.Length > 0)
				{

					using (var memoryStream = new MemoryStream())
					{
						await ImageFile.CopyToAsync(memoryStream);
						package.ImageData = memoryStream.ToArray();
					}
				}

				_context.TourPackages.Add(package);
				_context.SaveChanges();
				return RedirectToAction("Package", "Packages");
			}
			return View(package);
		}


		public async Task<IActionResult> EditPackage(int id)
		{
			var tour = await _context.TourPackages.FindAsync(id);
			if (tour == null) return NotFound();
			return View(tour);
		}

		[HttpPost]
		public async Task<IActionResult> EditPackage(int id, TourPackage model, IFormFile? imageFile)
		{
			var tour = await _context.TourPackages.FindAsync(id);
			if (tour == null) return NotFound();

			tour.Name = model.Name;
			tour.Destination = model.Destination;
			tour.Price = model.Price;
			tour.Description = model.Description;

			if (imageFile != null)
			{
				using var ms = new MemoryStream();
				await imageFile.CopyToAsync(ms);
				tour.ImageData = ms.ToArray();
			}

			_context.Update(tour);
			await _context.SaveChangesAsync();
			return RedirectToAction("Package", "Packages");
		}



		public async Task<IActionResult> DeletePackage(int id)
		{
			var tour = await _context.TourPackages.FindAsync(id);
			if (tour == null) return NotFound();

			_context.TourPackages.Remove(tour);
			await _context.SaveChangesAsync();
			return RedirectToAction("Package", "Packages");
		}






	}
}
