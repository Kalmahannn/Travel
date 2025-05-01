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







		[HttpGet]
		public async Task<IActionResult> EditBlog(int id)
		{
			var post = await _context.BlogPosts.FindAsync(id);
			if (post == null)
				return NotFound();

			ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name", post.CategoryId);
			return View(post); 
		}


		[HttpPost]
		public async Task<IActionResult> EditBlog(int id, BlogPost updatedPost, IFormFile? ImageFile)
		{
			var post = await _context.BlogPosts.FindAsync(id);
			if (post == null)
				return NotFound();

			post.Title = updatedPost.Title;
			post.Content = updatedPost.Content;
			post.CategoryId = updatedPost.CategoryId;

			if (ImageFile != null && ImageFile.Length > 0)
			{
				using var ms = new MemoryStream();
				await ImageFile.CopyToAsync(ms);
				post.ImageData = ms.ToArray();
			}

			await _context.SaveChangesAsync();
			return RedirectToAction("Blog","Blog");
		}


		[HttpGet]
		public async Task<IActionResult> DeleteBlog(int id)
		{
			var post = await _context.BlogPosts.FindAsync(id);
			if (post == null)
				return NotFound();

			_context.BlogPosts.Remove(post);
			await _context.SaveChangesAsync();

			return RedirectToAction("Blog","Blog");
		}




	}
}
