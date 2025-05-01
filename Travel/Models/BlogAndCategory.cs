using Travel.Controllers;

namespace Travel.Models
{
	public class BlogAndCategory
	{
		public ICollection<Category> categories { get; set; }
		public ICollection<BlogPost> blogs { get; set; }	
	}
}
