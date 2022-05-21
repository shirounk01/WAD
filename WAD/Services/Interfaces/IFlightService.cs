using WAD.Models;

namespace WAD.Services.Interfaces
{
    public interface IFlightService
    {
        List<Flight> GetFlightsByFilter(Filter filter);
        List<Flight> GetFlightsByModel(Flight flight);
        Flight SwapRoute(Flight flight);
        bool CheckTime(Flight flight, Time time);
        Flight GetFlightById(int id);
        void AddFlights(Flight flight);
    }
}
