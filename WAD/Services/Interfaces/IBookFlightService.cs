using WAD.Models;

namespace WAD.Services.Interfaces
{
    public interface IBookFlightService
    {
        void BookFlights(int goingId, int comingId);
        void BookFlight(User user, Flight flight);
    }
}
