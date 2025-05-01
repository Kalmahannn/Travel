using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Travel.Controllers;
using Travel.Data;
using Travel.Models;
using TravelistaMVC.Models;

namespace TravelistaMVC.Controllers
{
    public class BlogController : Controller
    {
        private readonly AppIdentityDbContext _context;


        public BlogController(AppIdentityDbContext context)
        {
            _context = context;
        }


        public  async Task<IActionResult> Index()
        {
            var _blogs = _context.BlogPosts.ToList();
			var _category = _context.Categories
	            .Include(c => c.BlogPosts)
	            .ToList();


			var blogCategory = new BlogAndCategory()
            {
                blogs = _blogs,
                categories = _category
            };
			return View(blogCategory);

        }

        [Route("Detail{blogId:int}")]
        public async Task<IActionResult> Details(int blogId)
        {

			var post = await _context.BlogPosts
			   .Include(p => p.Category)
			   .FirstOrDefaultAsync(p => p.Id == blogId);


			if (post == null)
				return NotFound();
			return View(post);
        }





		[Route("Blog/Info/{blogId:int}")]
		public async Task<IActionResult> Info(int blogId)
		{

			var post = await _context.BlogPosts
			   .Include(p => p.Category)
			   .FirstOrDefaultAsync(p => p.Id == blogId);


			if (post == null)
				return NotFound();
			return View(post);
		}






		public IActionResult Create()
		{
			ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name");
			return View();
		}

		public IActionResult GetImage(int id)
		{
			var blogs = _context.BlogPosts.Find(id);
			if (blogs == null || blogs.ImageData == null)
			{
				return NotFound();
			}

			return File(blogs.ImageData, "image/jpeg");
		}



	}
}
