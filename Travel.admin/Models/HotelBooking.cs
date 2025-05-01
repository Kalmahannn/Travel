namespace Travel.admin.Models
{
	public class HotelBooking
	{
		public int Id { get; set; }

		public int HotelId { get; set; }
		public Hotel Hotel { get; set; }

		public AppUser User { get; set; }
		public string UserId { get; set; }

		public DateTime CheckInDate { get; set; }
		public DateTime CheckOutDate { get; set; }
	}
}
