using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Travel.Api.Models;

namespace Travel.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PackageController : ControllerBase
	{
		private readonly AppIdentityDbContext _db;

		public PackageController(AppIdentityDbContext db)
		{
			_db = db;
		}

		[HttpGet]
		[Route("package")]
		public ActionResult<TourPackage> Package()
		{
			var packages = _db.TourPackages.ToList();
			return Ok(packages);
		}
	}
}
