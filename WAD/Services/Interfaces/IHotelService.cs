using WAD.Models;

namespace WAD.Services.Interfaces
{
    public interface IHotelService
    {
        List<Hotel> GetHotelsByModel(Hotel hotel);
        Hotel FindHotelById(int id);
        List<Hotel> FilterHotels(Filter filter, List<Hotel> hotels);
        List<Tuple<BookHotel, Hotel>> GetHotelsByReservations(List<BookHotel> bookedHotels);
    }
}
