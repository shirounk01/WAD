namespace WAD.Models.DTOs
{
    public class FlightInfo
    {
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public string? DepartureCity { get; set; }
        public string? ArrivalCity { get; set; }
    }
}
