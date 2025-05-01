using Microsoft.AspNetCore.Mvc;
using Travel.admin.Models;

namespace Travel.admin.Controllers
{
	public class CategoryController : Controller
	{
		private readonly AppIdentityDbContext _context;
		
		public CategoryController(AppIdentityDbContext context)
		{
			_context = context;
		}


		public IActionResult Category()
		{
			var categories = _context.Categories.ToList();	
			return View(categories);
		}

		
		
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(Category category)
		{
			if (ModelState.IsValid)
			{
				_context.Categories.Add(category);
				await _context.SaveChangesAsync();
				return RedirectToAction("Category","Category"); 
			}

			return View(category);
		}






	}
}
