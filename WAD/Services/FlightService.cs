using WAD.Models;
using WAD.Repositories.Interfaces;
using WAD.Services.Interfaces;

namespace WAD.Services
{
    public class FlightService : IFlightService
    {
        private readonly IRepositoryWrapper _repo;

        public FlightService(IRepositoryWrapper repo)
        {
            _repo = repo;
        }

        public void AddFlights(Flight flight)
        {
            _repo.FlightRepository.Create(flight);
            _repo.Save();
        }

        public void DeleteFlightById(int id)
        {
            var flight = _repo.FlightRepository.FindByCondition(item => item.FlightId == id).FirstOrDefault()!;
            _repo.FlightRepository.Delete(flight);
            _repo.Save();
        }

        public void UpdateFlightPriceById(int id, int price)
        {
            var flight = _repo.FlightRepository.FindByCondition(item => item.FlightId == id).FirstOrDefault()!;
            flight.Price = price;
            _repo.FlightRepository.Update(flight);
            _repo.Save();
        }

        public bool CheckTime(Flight flight, Time time)
        {
            bool result = false;
            switch (time)
            {
                case Time.Early:
                    result = 0 <= flight.DepartureTime.Hour && flight.DepartureTime.Hour < 6;
                    break;
                case Time.Morning:
                    result = 6 <= flight.DepartureTime.Hour && flight.DepartureTime.Hour < 12;
                    break;
                case Time.Afternoon:
                    result = 12 <= flight.DepartureTime.Hour && flight.DepartureTime.Hour < 18;
                    break;
                case Time.Evening:
                    result = 18 <= flight.DepartureTime.Hour && flight.DepartureTime.Hour < 24;
                    break;
                default:
                    break;
            }
            return result;
        }

        public Flight GetFlightById(int id)
        {
            var flight = _repo.FlightRepository.FindByCondition(item => item.FlightId == id).FirstOrDefault();
            return flight!;
        }

        public Flight GetFlightByDestination(string destination)
        {
            var flight = _repo.FlightRepository.FindByCondition(item => item.ArrivalCity == destination).FirstOrDefault();
            return flight!;
        }

        public List<Flight> GetFlightsByFilter(Filter filter)
        {
            var flights = _repo.FlightRepository.FindByFilter(filter);
            return flights;
        }

        public List<Flight> GetFlightsByModel(Flight flight)
        {
            List<Flight> flights = _repo.FlightRepository.FindByModel(flight);
            return flights;
        }

        public List<Tuple<BookFlight, Flight>> GetHotelsByReservations(List<BookFlight> bookedFlights)
        {
            List<Tuple<BookFlight, Flight>> flightsHistory = new List<Tuple<BookFlight, Flight>>();
            foreach (var flight in bookedFlights)
            {
                var entry = _repo.FlightRepository.FindByCondition(item=>item.FlightId == flight.FlightId).FirstOrDefault();
                flightsHistory.Add(Tuple.Create(flight, entry)!);
            }
            return flightsHistory;
        }

        public Flight SwapRoute(Flight flight)
        {
            var newFlight = flight;
            (newFlight.DepartureCity, newFlight.ArrivalCity) = (newFlight.ArrivalCity, newFlight.DepartureCity);
            (newFlight.DepartureTime, newFlight.ArrivalTime) = (newFlight.ArrivalTime, newFlight.DepartureTime);
            return newFlight;
        }
    }
}
