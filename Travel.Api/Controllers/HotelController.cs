using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Travel.Api.Models;

namespace Travel.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class HotelController : ControllerBase
	{
		private readonly AppIdentityDbContext _db;
		private readonly ILogger<HotelController> _logger;

		public HotelController(AppIdentityDbContext db, ILogger<HotelController> logger) {
			_db = db;
			_logger = logger;
		}

		[HttpGet]
		public List<Hotel> GetHotel()
		{
			
			return _db.Hotels.ToList();
		}

		[HttpGet]
		[Route("GetById/{hotelId:int}")]
		public async Task<ActionResult<Hotel>> Details(int hotelId)
		{
			var hotel = await _db.Hotels
				.FirstOrDefaultAsync(h => h.Id == hotelId);

			if (hotel == null)
			{
				return NotFound();
			}

			return Ok(hotel);
		}

	}
}
