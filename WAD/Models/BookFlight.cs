namespace WAD.Models
{
    public class BookFlight
    {
        public int BookFlightId { get; set; }
        public int UserId { get; set; }
        public int FlightId { get; set; }
        public DateTime RegistrationDate { get; set; }

        public User? User { get; set; }
        public Flight? Flight { get; set; }
    }
}
