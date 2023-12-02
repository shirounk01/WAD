namespace WAD.Models
{
    public class Flight
    {
        public int FlightId { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public string? DepartureCity { get; set; }
        public string? DepartureAirport { get; set; }
        public string? ArrivalCity { get; set; }
        public string? ArrivalAirport { get; set; }
        public int Price { get; set; }
        public ICollection<BookFlight>? BookingUsers { get; set; }

    }
}
