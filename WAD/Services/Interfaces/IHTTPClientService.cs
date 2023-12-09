using WAD.Models;

namespace WAD.Services.Interfaces
{
    public interface IHTTPClientService
    {
        Task<List<Hotel>> GetHotelsByModel(Hotel hotel);
        Task<List<Hotel>> FilterHotels(Filter filter, List<Hotel> hotels);
    }
}