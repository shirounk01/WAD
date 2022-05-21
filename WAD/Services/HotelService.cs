using WAD.Models;
using WAD.Repositories.Interfaces;
using WAD.Services.Interfaces;

namespace WAD.Services
{
    public class HotelService : IHotelService
    {
        private IRepositoryWrapper _repo;

        public HotelService(IRepositoryWrapper repo)
        {
            _repo = repo;
        }

        public List<Hotel> FilterHotels(Filter filter, List<Hotel> hotels)
        {
            var result = hotels.Where(item => (filter.MinPrice == 0 || item.Price >= filter.MinPrice) && (filter.MaxPrice == 0 || item.Price <= filter.MaxPrice));
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

        public Hotel FindHotelById(int id)
        {
            var hotel = _repo.HotelRepository.FindByCondition(item => item.HotelId == id).FirstOrDefault();
            return hotel;
        }

        public List<Hotel> GetHotelsByModel(Hotel hotel)
        {
            var hotels = _repo.HotelRepository.FindByModel(hotel);
            return hotels;
        }
    }
}
