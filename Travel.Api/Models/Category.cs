using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Travel.Api.Models
{
	public class Category
	{
		public int Id { get; set; }
		public string Name { get; set; }

		[ValidateNever]
		public ICollection<BlogPost> BlogPosts { get; set; }
	}
}
