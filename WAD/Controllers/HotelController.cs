using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WAD.Models;
using WAD.Services.Interfaces;
using WAD.ViewModels;

namespace WAD.Controllers
{
    public class HotelController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHotelService _hotelService;
        public readonly IBookHotelService _bookHotelService;
        public readonly IReviewService _reviewService;

        public HotelController(ILogger<HomeController> logger, IHotelService hotelService, IBookHotelService bookHotelService, IReviewService reviewService)
        {
            _logger = logger;
            _hotelService = hotelService;
            _bookHotelService = bookHotelService;
            _reviewService = reviewService;
        }

        public IActionResult Index(Hotel hotel)
        {
            var results = _hotelService.GetHotelsByModel(hotel);
            TempData["HotelPacks"] = JsonConvert.SerializeObject(results);
            TempData["HotelModel"] = JsonConvert.SerializeObject(hotel);
            return View(results);
        }
        [HttpPost]
        public IActionResult Index([FromForm] Filter filter)
        {
            var hotels = JsonConvert.DeserializeObject<List<Hotel>>(TempData["HotelPacks"].ToString());
            hotels = _hotelService.FilterHotels(filter, hotels);
            TempData["HotelPacks"] = JsonConvert.SerializeObject(hotels);
            return View(hotels);
        }
        public IActionResult ResetIndex()
        {
            var hotel = JsonConvert.DeserializeObject<Hotel>(TempData["HotelModel"].ToString());
            return RedirectToAction("Index", hotel);
        }
        public IActionResult BookHotel(int id)
        {
            var hotel = JsonConvert.DeserializeObject<Hotel>(TempData["HotelModel"].ToString());
            _bookHotelService.BookHotel(id, hotel);
            return RedirectToAction("Index", hotel);
        }

        public IActionResult Review(int id)
        {
            Hotel hotel = _hotelService.FindHotelById(id);
            List<Review> reviews = _reviewService.FindReviewsByHotelId(id);
            Reviews viewReviews = new Reviews { Hotel = hotel, Posts = reviews, Review = new Review() };

            return View(viewReviews);
        }

        [HttpPost]
        public IActionResult Review([FromForm] Review review, int id)
        {
            _reviewService.AddReview(review, id);
            return RedirectToAction("Review", id);
        }
    }
}
