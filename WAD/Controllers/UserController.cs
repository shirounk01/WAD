#nullable disable
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WAD.Models;
using WAD.Models.DTOs;
using WAD.Services.Interfaces;
using WAD.ViewModels;

namespace WAD.Controllers
{
    public class UserController : Controller
    {
        private readonly IBookFlightService _bookFlightService;
        private readonly IBookHotelService _bookHotelService;
        private readonly IFlightService _flightService;
        private readonly IHotelService _hotelService;
        private readonly UserManager<IdentityUser> _userManager;

        private readonly IHTTPClientService _clientService;


        public UserController(IBookFlightService bookFlightService, IBookHotelService bookHotelService, IFlightService flightService, IHotelService hotelService, UserManager<IdentityUser> userManager, IHTTPClientService clientService)
        {
            _bookFlightService = bookFlightService;
            _bookHotelService = bookHotelService;
            _flightService = flightService;
            _hotelService = hotelService;
            _userManager = userManager;
            _clientService = clientService;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserInfo userInfo)
        {
            var token = await _clientService.Login(userInfo);
            HttpContext.Session.SetString("token", token);
            
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Profile()
        {
            var userGuid = _userManager.GetUserId(HttpContext.User);
            var bookedHotels = _bookHotelService.GetReservationsByUserId(userGuid);
            var bookedFlights = _bookFlightService.GetReservationsByUserId(userGuid);
            List<Tuple<BookHotel, Hotel>> hotelHistory = _hotelService.GetHotelsByReservations(bookedHotels);
            List<Tuple<BookFlight, Flight>> flightHistory = _flightService.GetHotelsByReservations(bookedFlights);
            History history = new History();
            history.Hotels = hotelHistory;
            history.Flights = flightHistory;
            return View(history);
        }
    }
}
