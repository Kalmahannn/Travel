using Microsoft.AspNetCore.Mvc;
using Travel.Controllers;

namespace TravelistaMVC.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            var posts = new List<BlogPost>
            {
                new BlogPost
                {
                    Id = 1,
                    Title = "Astronomy Binoculars",
                    Author = "Mark Wiens",
                    PublishedDate = DateTime.Now.AddDays(-7),
                    Category = "Technology",
                    Content = "Content about Astronomy Binoculars...",
                    ImageUrl = "/img/blog/feature-img1.jpg"
                },
                new BlogPost
                {
                    Id = 2,
                    Title = "Travel Tips",
                    Author = "John Doe",
                    PublishedDate = DateTime.Now.AddDays(-10),
                    Category = "Travel",
                    Content = "Useful travel tips content...",
                    ImageUrl = "/img/blog/feature-img2.jpg"
                }
            };

            return View(posts);
        }

        public IActionResult Details(int id)
        {
            var post = new BlogPost
            {
                Id = 1,
                Title = "Astronomy Binoculars",
                Author = "Mark Wiens",
                PublishedDate = DateTime.Now.AddDays(-7),
                Category = "Technology",
                Content = "Detailed content about Astronomy Binoculars...",
                ImageUrl = "/img/blog/feature-img1.jpg"
            };

            return View(post);
        }
    }
}
