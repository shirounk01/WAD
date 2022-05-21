namespace WAD.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public ICollection<BookHotel>? BookedHotels { get; set; }
        public ICollection<BookFlight>? BookedFlight { get; set; }
        public ICollection<Review>? Reviews { get; set; }
    }
}
