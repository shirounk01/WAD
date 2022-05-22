using WAD.Models;

namespace WAD.ViewModels
{
	public class History
	{
		public List<Tuple<BookFlight, Flight>> Flights { get; set; }
		public List<Tuple<BookHotel, Hotel>> Hotels { get; set; }
	}
}
