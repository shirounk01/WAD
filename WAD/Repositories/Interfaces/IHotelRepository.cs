using WAD.Models;

namespace WAD.Repositories.Interfaces
{
    public interface IHotelRepository : IRepositoryBase<Hotel>
    {
        List<Hotel> FindByModel(Hotel hotel);
    }
}
