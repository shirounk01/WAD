using WAD.Models;
using WAD.Services.Interfaces;

namespace WAD.Services
{
    public class FlightPackService : IFlightPackService
    {
        private readonly IFlightService _flighService;

        public FlightPackService(IFlightService flightService)
        {
            _flighService = flightService;
        }

        public List<FlightPack> FilterFlights(Filter filter, List<FlightPack> flights)
        {
            var result = flights.Where(item => (filter.DepartureTime == 0 || _flighService.CheckTime(item.FlightGoing!, filter.DepartureTime)) && (filter.ArrivalTime == 0 || _flighService.CheckTime(item.FlightComing!, filter.ArrivalTime)));

            result = result.Where(item => (filter.MinPrice == 0 || item.Price >= filter.MinPrice) && (filter.MaxPrice == 0 || item.Price <= filter.MaxPrice));
            if (filter.sort != 0)
            {
                if (filter.sort == Sort.Ascending)
                {
                    result = result.OrderBy(item => item.Price);
                }
                else
                {
                    result = result.OrderByDescending(item => item.Price);
                }
            }
            return result.ToList();
        }

        public List<FlightPack> GeneratePack(List<Flight> flightsGoing, List<Flight> flightsComing)
        {
            List<FlightPack> flights = new List<FlightPack>();
            foreach (var going in flightsGoing)
            {
                foreach (var coming in flightsComing)
                {
                    flights.Add(new FlightPack { FlightComing = coming, FlightGoing = going, Price = coming.Price + going.Price });
                }
            }
            return flights;
        }

    }
}
