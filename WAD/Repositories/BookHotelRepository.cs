using WAD.Models;
using WAD.Repositories.Interfaces;

namespace WAD.Repositories
{
    public class BookHotelRepository : RepositoryBase<BookHotel>, IBookHotelRepository
    {
        public BookHotelRepository(BookNGoContext locationContext) : base(locationContext)
        {
        }
    }
}
