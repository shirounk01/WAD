namespace WAD.Models
{
    public class BookHotel
    {
        public int BookHotelId { get; set; }
        //public int UserId { get; set; }
        public string UserGuid { get; set; }
        public int HotelId { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int Price { get; set; }
        //public User? User { get; set; }
        public Hotel? Hotel { get; set; }
    }
}
