namespace WAD.Models
{
    public class Hotel
    {
        public int HotelId { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Image { get; set; }
        public int Price { get; set; }
        public DateTime OpenDate { get; set; } = DateTime.MinValue;
        public DateTime CloseDate { get; set; } = DateTime.MaxValue;

        public ICollection<BookHotel>? BookingUsers { get; set; }
        public ICollection<Review>? Reviews { get; set; }
    }
}
