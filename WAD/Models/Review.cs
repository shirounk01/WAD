namespace WAD.Models
{
    public class Review
    {

        public int ReviewId { get; set; }
		public string? UserGuid { get; set; }
		public int HotelId { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime Created { get; set; }
        public Hotel? Hotel { get; set; }
    }
}
