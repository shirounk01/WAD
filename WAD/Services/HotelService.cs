using WAD.Models;
using WAD.Repositories.Interfaces;
using WAD.Services.Interfaces;

namespace WAD.Services
{
    public class HotelService : IHotelService
    {
        private readonly IRepositoryWrapper _repo;

        public HotelService(IRepositoryWrapper repo)
        {
            _repo = repo;
        }

        public void DeleteHotelById(int id)
        {
            var hotel = _repo.HotelRepository.FindByCondition(item => item.HotelId == id).FirstOrDefault()!;
            _repo.HotelRepository.Update(hotel);
            _repo.Save();
        }

        public void UpdateHotelPriceById(int id, int price)
        {
            var hotel = _repo.HotelRepository.FindByCondition(item => item.HotelId == id).FirstOrDefault()!;
            hotel.Price = price;
            _repo.HotelRepository.Update(hotel);
            _repo.Save();
        }

        public List<Hotel> FilterHotels(Filter filter, List<Hotel> hotels)
        {
            var result = hotels.Where(item => (filter.MinPrice == 0 || item.Price >= filter.MinPrice) && (filter.MaxPrice == 0 || item.Price <= filter.MaxPrice));
            result = SortHotelsList(filter, result);
            return result.ToList();
        }

        private static IEnumerable<Hotel> SortHotelsList(Filter filter, IEnumerable<Hotel> result)
        {
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

            return result;
        }

        public Hotel FindHotelById(int id)
        {
            var hotel = _repo.HotelRepository.FindByCondition(item => item.HotelId == id).FirstOrDefault();
            return hotel!;
        }

        public List<Hotel> GetHotelsByModel(Hotel hotel)
        {
            var hotels = _repo.HotelRepository.FindByModel(hotel);
            return hotels;
        }

        public List<Tuple<BookHotel, Hotel>> GetHotelsByReservations(List<BookHotel> bookedHotels)
        {
            List<Tuple<BookHotel, Hotel>> hotelsHistory = new List<Tuple<BookHotel, Hotel>>();
            BuildHotelsHistory(bookedHotels, hotelsHistory);
            return hotelsHistory;
        }

        private void BuildHotelsHistory(List<BookHotel> bookedHotels, List<Tuple<BookHotel, Hotel>> hotelsHistory)
        {
            foreach (var hotel in bookedHotels)
            {
                var entry = _repo.HotelRepository.FindByCondition(item => item.HotelId == hotel.HotelId).FirstOrDefault();
                hotelsHistory.Add(Tuple.Create(hotel, entry)!);
            }
        }
    }
}
