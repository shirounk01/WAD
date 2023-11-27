namespace WAD.Models
{
    public class BookFlight
    {
        public int BookFlightId { get; set; }
        public string? UserGuid { get; set; }
        public int FlightId { get; set; }
        public DateTime RegistrationDate { get; set; }
        public Flight? Flight { get; set; }
    }
}
