using WAD.Models;

namespace WAD.Repositories.Interfaces
{
    public interface IFlightRepository : IRepositoryBase<Flight>
    {
        List<Flight> FindByFilter(Filter filter);
        List<Flight> FindByModel(Flight flight);
    }
}
