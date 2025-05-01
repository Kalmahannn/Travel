namespace Travel.admin.Models
{
	public class TourPackage
	{
		public int Id { get; set; }

		// Туристік пакеттің атауы
		public string Name { get; set; }

		// Туристік бағыт
		public string Destination { get; set; }

		// Тур бағасы
		public decimal Price { get; set; }

		// Тур туралы қысқаша сипаттама
		public string Description { get; set; }

		// Пакет суреті
		public string ImageUrl { get; set; }
	}
}
