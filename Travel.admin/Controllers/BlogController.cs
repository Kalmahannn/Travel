using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using Travel.admin.Models;

namespace Travel.admin.Controllers
{
	public class BlogController : Controller
	{
		private readonly ILogger _logger;
		private readonly AppIdentityDbContext _context;

		public BlogController(AppIdentityDbContext contex, ILogger<BlogController> logger)
		{
			_context = contex;
			_logger = logger;
		}



		public IActionResult Blog()
		{
			var blogs = _context.BlogPosts.Include(b => b.Category).ToList();
			return View(blogs);
		}

		public IActionResult Create()
		{
			ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name");
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(BlogPost blog,IFormFile ImageFile)
		{
			if (ModelState.IsValid)
			{

				if (ImageFile != null && ImageFile.Length > 0)
				{

					using (var memoryStream = new MemoryStream())
					{
						await ImageFile.CopyToAsync(memoryStream);
						blog.ImageData = memoryStream.ToArray();
					}
				}

				_context.BlogPosts.Add(blog);
				_context.SaveChanges();
				return RedirectToAction("Blog", "Blog");
			}
			ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name", blog.CategoryId);

			return View(blog);
		}

	}
}
