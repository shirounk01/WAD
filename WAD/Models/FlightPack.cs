using WAD.Models;

namespace WAD.Models
{
    public class FlightPack
    {
        public Flight? FlightGoing{ get; set; }
        public Flight? FlightComing { get; set; }
        public int Price { get; set; }
    }
}
