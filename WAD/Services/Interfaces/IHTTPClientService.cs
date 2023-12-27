using WAD.Models;
using WAD.Models.DTOs;
using WAD.ViewModels;

namespace WAD.Services.Interfaces
{
    public interface IHTTPClientService
    {
        Task<List<Hotel>> GetHotelsByModel(Hotel hotel);
        Task<List<Hotel>> FilterHotels(Filter filter, List<Hotel> hotels);
        Task<dynamic> GetFlightsByInfo(FlightInfo flightInfo);
        Task<List<FlightPack>> FilterFlights(Filter filter, List<FlightPack> flights);
        Task<int> GetRates(string currency);
        Task<string> Login(UserInfo userInfo);
        Task BookHotel(int id, Hotel hotel);
        Task<dynamic> Review(int id);
        Task AddReview(int id, Review review);
        Task BookFlight(int goingId, int comingId);
        Task<History> GetHistory();
        Task<string> Register(UserInfo userInfo);
    }
}