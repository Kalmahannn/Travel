using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using Travel.Controllers;
using Travel.Data;

namespace TravelistaMVC.Controllers
{
    public class BlogController : Controller
    {
        private readonly AppIdentityDbContext _context;


        public BlogController(AppIdentityDbContext context)
        {
            _context = context;
        }


        public  IActionResult Index()
        {
            var posts = _context.BlogPosts.ToList();
            return View(posts);
        }

        [Route("Dateil{blogId:int}")]
        public IActionResult Details(int blogId)
        {
           

            return View();
        }


		public IActionResult Create()
		{
			ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name");
			return View();
		}



	}
}
