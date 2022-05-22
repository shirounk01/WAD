using WAD.Models;

namespace WAD.Services.Interfaces
{
    public interface IBookFlightService
    {

        void BookFlights(int goingId, int comingId, string userGuid);
        void BookFlight(string userGuid, Flight flight);
        List<BookFlight> GetReservationsByUserId(string userGuid);
    }
}
