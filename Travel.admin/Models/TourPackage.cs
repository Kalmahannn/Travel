namespace Travel.admin.Models
{
	public class TourPackage
	{
		public int Id { get; set; }

	
		public string Name { get; set; }


		public string Destination { get; set; }

	
		public decimal Price { get; set; }


		public string Description { get; set; }


		public Byte[]? ImageData { get; set; }
	}
}
