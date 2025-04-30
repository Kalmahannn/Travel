using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Travel.Api.Models;

namespace Travel.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class HotelController : ControllerBase
	{
		private readonly AppDbContext _db;

		public HotelController(AppDbContext db) {
			_db = db; 
		}

		[HttpGet]
		public List<Hotel> GetHotel()
		{
			;
			return _db.Hotels.ToList();
		}
	}
}
