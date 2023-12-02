using WAD.Models;
using WAD.Repositories.Interfaces;

namespace WAD.Repositories
{
    public class FlightRepository : RepositoryBase<Flight>, IFlightRepository
    {
        public FlightRepository(BookNGoContext locationContext) : base(locationContext)
        {
        }
       
        public List<Flight> FindByFilter(Filter filter)
        {
            var result = Context.Flights!.AsQueryable();

            return result.ToList();
        }

        public List<Flight> FindByModel(Flight flight)
        {
            var result = Context.Flights!.AsQueryable();

            result = result.Where(item => item.DepartureTime.Date.Equals(flight.DepartureTime.Date) && item.DepartureCity == flight.DepartureCity && item.ArrivalCity == flight.ArrivalCity);

            return result.ToList();
        }
    }
}
