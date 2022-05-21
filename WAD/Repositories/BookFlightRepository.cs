using WAD.Models;
using WAD.Repositories.Interfaces;

namespace WAD.Repositories
{
    public class BookFlightRepository : RepositoryBase<BookFlight>, IBookFlightRepository
    {
        public BookFlightRepository(BookNGoContext locationContext) : base(locationContext)
        {
        }
    }
}
