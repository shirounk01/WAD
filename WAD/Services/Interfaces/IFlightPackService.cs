using WAD.Models;

namespace WAD.Services.Interfaces
{
    public interface IFlightPackService
    {
        List<FlightPack> GeneratePack(List<Flight> flightsGoing, List<Flight> flightsComing);
        List<FlightPack> FilterFlights(Filter filter, List<FlightPack> flights);
    }
}
