namespace Travel.admin.Models
{
	public class Hotel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public byte[]? ImageData { get; set; }
		public string Location { get; set; }
		public decimal PricePerNight { get; set; }
		public int Stars { get; set; }
		public bool SwimmingPool { get; set; }
		public bool Gymnasium { get; set; }
		public bool Wifi { get; set; }
		public bool RoomService { get; set; }
		public bool AirCondition { get; set; }
		public bool Restaurant { get; set; }
	}
}
