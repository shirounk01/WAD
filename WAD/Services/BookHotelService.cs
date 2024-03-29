﻿using WAD.Models;
using WAD.Repositories.Interfaces;
using WAD.Services.Interfaces;

namespace WAD.Services
{
    public class BookHotelService : IBookHotelService
    {
        private readonly IRepositoryWrapper _repo;
        private readonly IHotelService _hotelService;

        public BookHotelService(IRepositoryWrapper repo, IHotelService hotelService)
        {
            _repo = repo;
            _hotelService = hotelService;
        }

        public void BookHotel(int id, string userGuid, Hotel reference)
        {
            Hotel hotel = _hotelService.FindHotelById(id);
            BookHotel bookHotel = BuildNewBookHotel(userGuid, reference, hotel);

            _repo.HotelRepository.Update(hotel);
            _repo.BookHotelRepository.Create(bookHotel);

            _repo.Save();
        }

        private static BookHotel BuildNewBookHotel(string userGuid, Hotel reference, Hotel hotel)
        {
            BookHotel bookHotel = new BookHotel();
            bookHotel.UserGuid = userGuid;
            bookHotel.Hotel = hotel;
            bookHotel.RegistrationDate = DateTime.Now;
            bookHotel.CheckInDate = reference.OpenDate;
            bookHotel.CheckOutDate = reference.CloseDate;
            bookHotel.Price = hotel.Price * ((int)(reference.CloseDate - reference.OpenDate).TotalDays);
            return bookHotel;
        }

        public List<BookHotel> GetReservationsByUserId(string userGuid)
        {
            List<BookHotel> hotelReservations = _repo.BookHotelRepository.FindByCondition(hotel => hotel.UserGuid == userGuid).ToList();
            return hotelReservations;
        }
    }
}
