using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WAD.Models;
using WAD.Models.DTOs;
using WAD.Services.Interfaces;
using WAD.ViewModels;

namespace WAD.Controllers
{
    public class FlightController : Controller
    {
        private readonly IFlightService _flightService;
        private readonly IFlightPackService _flightPackService;
        private readonly IBookFlightService _bookFlightService;
        private readonly UserManager<IdentityUser> _userManager;

        private readonly IHTTPClientService _clientService;

        public FlightController(IFlightService flightService, IFlightPackService flightPackService, IBookFlightService bookFlightService, UserManager<IdentityUser> userManager, IHTTPClientService clientService)
        {
            _flightService = flightService;
            _flightPackService = flightPackService;
            _bookFlightService = bookFlightService;
            _userManager = userManager;
            _clientService = clientService;
        }

        public async Task<IActionResult> Index(FlightInfo flightInfo)
        {
            //var reference = flight;
            //List<Flight> flightsGoing = _flightService.GetFlightsByModel(reference);
            //reference = _flightService.SwapRoute(reference);
            //List<Flight> flightsComing = _flightService.GetFlightsByModel(reference);
            //var flightPacks = _flightPackService.GeneratePack(flightsGoing, flightsComing);
            var results = await _clientService.GetFlightsByInfo(flightInfo);
            var flight = JsonConvert.DeserializeObject<Flight>(results.flight.ToString());
            var flightPacks = JsonConvert.DeserializeObject<List<FlightPack>>(results.flightPack.ToString());
            TempData["FlightPacks"] = JsonConvert.SerializeObject(flightPacks);
            TempData["FlightModel"] = JsonConvert.SerializeObject(flight);
            return View(flightPacks);
        }
        [HttpPost]
        public async Task<IActionResult> Index([FromForm] Filter filter)
        {
            var flights = JsonConvert.DeserializeObject<List<FlightPack>>(TempData["FlightPacks"]!.ToString()!);

            //flights = _flightPackService.FilterFlights(filter, flights!);
            flights = await _clientService.FilterFlights(filter, flights);
            TempData["FlightPacks"] = JsonConvert.SerializeObject(flights);
            return View(flights);
        }

        public IActionResult ResetIndex()
        {
            var flight = JsonConvert.DeserializeObject<Flight>(TempData["FlightModel"]!.ToString()!);
            return RedirectToAction("Index", flight);
        }
        public async Task<IActionResult> BookFlight(int goingId, int comingId)
        {
            //string userGuid = _userManager.GetUserId(HttpContext.User);
            //_bookFlightService.BookFlights(goingId, comingId, userGuid);
            await _clientService.BookFlight(goingId, comingId);
            return RedirectToAction("ResetIndex");
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Create([FromForm] Flight flight)
        {
            _flightService.AddFlights(flight);
            return RedirectToAction("Index", "Home");
        }
    }
}
