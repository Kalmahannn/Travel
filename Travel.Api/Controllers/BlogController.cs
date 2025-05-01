using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Travel.Api.Models;

namespace Travel.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BlogController : ControllerBase
	{
		private readonly ILogger<BlogController> _logger;
		private readonly AppIdentityDbContext _context;

		public BlogController(ILogger<BlogController> logger,AppIdentityDbContext context)
		{
			_logger = logger;
			_context = context;
		}



		[HttpGet]
		[Route("blog")]
		public IActionResult GetAllPosts()
		{
			var posts = _context.BlogPosts
				.Include(p => p.Category)
				.Select(p => new
				{
					p.Id,
					p.Title,
					p.Author,
					p.Description,
					p.PublishedDate,
					Category = p.Category != null ? p.Category.Name : null
				})
				.ToList();

			return Ok(posts);
		}

		[HttpGet("GetBlog{id:int}")]
		public async Task<IActionResult> GetBlogPostById(int id)
		{
			var post = await _context.BlogPosts
				.Include(p => p.Category)
				.Where(p => p.Id == id)
				.Select(p => new
				{
					p.Id,
					p.Title,
					p.Description,
					p.Content,
					p.Author,
					p.PublishedDate,
					Category = p.Category == null ? p.Category.Name : null
					
				})
				.FirstOrDefaultAsync();

			if (post == null)
				return NotFound();

			return Ok(post);
		}


	}
}
