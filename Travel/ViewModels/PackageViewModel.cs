using TravelistaMVC.Models;

namespace TravelistaMVC.ViewModels
{
    public class PackageViewModel
    {
        public List<TourPackage> Packages { get; set; }
        public HomeAbout AboutSection { get; set; }
    }

    public class HomeAbout
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ButtonText { get; set; }
        public string ImageUrl { get; set; }
    }
}
