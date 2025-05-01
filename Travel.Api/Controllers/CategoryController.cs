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
		private readonly ILogger<CategoryController> _logger;

		public CategoryController(AppIdentityDbContext db, ILogger<CategoryController> logger)
		{
			_db = db;
			_logger = logger;
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
