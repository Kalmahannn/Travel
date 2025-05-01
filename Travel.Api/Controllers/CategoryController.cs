using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Travel.Api.Models;

namespace Travel.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoryController : ControllerBase
	{
		private readonly AppIdentityDbContext _db;

		public CategoryController(AppIdentityDbContext db)
		{
			_db = db;
		}

		[HttpGet]
		[Route("category")]
		public IActionResult GetAllCategories()
		{
			var categories = _db.Categories
				.Select(c => new
				{
					c.Id,
					c.Name
				})
				.ToList();

			return Ok(categories);
		}
	}
}
