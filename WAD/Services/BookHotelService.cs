using WAD.Models;
using WAD.Repositories.Interfaces;
using WAD.Services.Interfaces;

namespace WAD.Services
{
    public class BookHotelService:IBookHotelService
    {
        private IRepositoryWrapper _repo;
        private IHotelService _hotelService;

        public BookHotelService(IRepositoryWrapper repo, IHotelService hotelService)
        {
            _repo = repo;
            _hotelService = hotelService;
        }

        public void BookHotel(int id, Hotel reference)
        {
            User user = _repo.UserRepository.FindAll().FirstOrDefault();
            Hotel hotel = _hotelService.FindHotelById(id);

            BookHotel bookHotel = new BookHotel();
            bookHotel.User = user;
            bookHotel.Hotel= hotel;
            bookHotel.RegistrationDate = DateTime.Now;
            bookHotel.CheckInDate = reference.OpenDate;
            bookHotel.CheckOutDate = reference.CloseDate;
            bookHotel.Price=hotel.Price*((int)(reference.CloseDate-reference.OpenDate).TotalDays);

            _repo.UserRepository.Update(user);
            _repo.HotelRepository.Update(hotel);
            _repo.BookHotelRepository.Create(bookHotel);

            _repo.Save();
        }
    }
}
