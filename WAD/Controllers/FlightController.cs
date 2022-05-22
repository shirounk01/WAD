using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WAD.Models;
using WAD.Services.Interfaces;
using WAD.ViewModels;

namespace WAD.Controllers
{
    public class FlightController : Controller
    {
        private readonly ILogger<FlightController> _logger;
        private readonly IFlightService _flightService;
        private readonly IFlightPackService _flightPackService;
        private readonly IBookFlightService _bookFlightService;
        private readonly UserManager<IdentityUser> _userManager;

        public FlightController(ILogger<FlightController> logger, IFlightService flightService, IFlightPackService flightPackService, IBookFlightService bookFlightService, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _flightService = flightService;
            _flightPackService = flightPackService;
            _bookFlightService = bookFlightService;
            _userManager = userManager;
        }

        public IActionResult Index(Flight flight)
        {
            var reference = flight;
            List<Flight> flightsGoing = _flightService.GetFlightsByModel(reference);
            reference = _flightService.SwapRoute(reference);
            List<Flight> flightsComing = _flightService.GetFlightsByModel(reference);
            var flightPacks = _flightPackService.GeneratePack(flightsGoing, flightsComing);
            TempData["FlightPacks"] = JsonConvert.SerializeObject(flightPacks);
            TempData["FlightModel"] = JsonConvert.SerializeObject(flight);
            return View(flightPacks);
        }
        [HttpPost]
        public IActionResult Index([FromForm] Filter filter)
        {
            var flights = JsonConvert.DeserializeObject<List<FlightPack>>(TempData["FlightPacks"].ToString());
            flights = _flightPackService.FilterFlights(filter, flights);
            TempData["FlightPacks"] = JsonConvert.SerializeObject(flights);
            return View(flights);
        }

        public IActionResult ResetIndex()
        {
            var flight = JsonConvert.DeserializeObject<Flight>(TempData["FlightModel"].ToString());
            return RedirectToAction("Index", flight);
        }
        [Authorize]
        public IActionResult BookFlight(int goingId, int comingId)
        {
            string userGuid = _userManager.GetUserId(HttpContext.User);
            _bookFlightService.BookFlights(goingId, comingId, userGuid);
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
