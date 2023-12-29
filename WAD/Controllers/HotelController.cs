using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WAD.Models;
using WAD.Services.Interfaces;
using WAD.ViewModels;

namespace WAD.Controllers
{
    public class HotelController : Controller
    {
        private readonly IHotelService _hotelService;
        public readonly IBookHotelService _bookHotelService;
        public readonly IReviewService _reviewService;
        private readonly UserManager<IdentityUser> _userManager;

        private readonly IHTTPClientService _clientService;
        private readonly IConfiguration _config;


        public HotelController(IHotelService hotelService, IBookHotelService bookHotelService, IReviewService reviewService, UserManager<IdentityUser> userManager, IHTTPClientService clientService, IConfiguration config)
        {
            _hotelService = hotelService;
            _bookHotelService = bookHotelService;
            _reviewService = reviewService;
            _userManager = userManager;
            _clientService = clientService;
            _config = config;
        }

        public async Task<IActionResult> Index(Hotel hotel)
        {
            var results = await _clientService.GetHotelsByModel(hotel);
            //foreach (var result in results)
            //{
            //    result.Price *= 100;
            //}
            ////var results = _hotelService.GetHotelsByModel(hotel);
            //TempData["CurrencyRatio"] = 100;
            //TempData["CurrencySymbol"] = "S";
            TempData["HotelPacks"] = JsonConvert.SerializeObject(results);
            TempData["HotelModel"] = JsonConvert.SerializeObject(hotel);
            //TempData["LastRate"] = "S";
            return View(results);
        }
        [HttpPost]
        public async Task<IActionResult> Index([FromForm] Filter filter)
        {
            var hotels = JsonConvert.DeserializeObject<List<Hotel>>(TempData["HotelPacks"]!.ToString()!);
            //filter.MinPrice *= 100;
            //filter.MaxPrice *= 100;
            //hotels = _hotelService.FilterHotels(filter, hotels!);
            hotels = await _clientService.FilterHotels(filter, hotels);
            //if (filter.currency != Currency.None)
            //{
            //    var currencyRatio = JsonConvert.DeserializeObject<int>(TempData["CurrencyRatio"].ToString());
            //    var newCurrencyRation = await _clientService.GetRates(filter.currency.ToString());
            //    foreach (var hotel in hotels)
            //    {
            //        hotel.Price = (int)(hotel.Price / currencyRatio * newCurrencyRation);
            //    }
            //    TempData["CurrencyRatio"] = newCurrencyRation;
            //    TempData["LastRate"] = _config.GetSection($"CurrencySymbol:{filter.currency.ToString()}").Get<string>();
            //    TempData["CurrencySymbol"] = _config.GetSection($"CurrencySymbol:{filter.currency.ToString()}").Get<string>();
            //}
            //else
            //{
            //    var lastRate = TempData["LastRate"];
            //    TempData["CurrencySymbol"] = lastRate;
            //    TempData["LastRate"] = lastRate;
            //}
            TempData["HotelPacks"] = JsonConvert.SerializeObject(hotels);
            return View(hotels);
        }
        public IActionResult ResetIndex()
        {
            var hotel = JsonConvert.DeserializeObject<Hotel>(TempData["HotelModel"]!.ToString()!);
            TempData["HotelModel"] = JsonConvert.SerializeObject(hotel);
            return RedirectToAction("Index", hotel);
        }

        public async Task<IActionResult> BookHotel(int id)
        {
            var hotel = JsonConvert.DeserializeObject<Hotel>(TempData["HotelModel"]!.ToString()!);
            //string userGuid = _userManager.GetUserId(HttpContext.User);
            //_bookHotelService.BookHotel(id, userGuid, hotel!);
            await _clientService.BookHotel(id, hotel!);
            return RedirectToAction("Index", hotel);
        }

        public async Task<IActionResult> Review(int id)
        {
            //Hotel hotel = _hotelService.FindHotelById(id);
            //List<Review> reviews = _reviewService.FindReviewsByHotelId(id);
            var dataObj = await _clientService.Review(id);
            var hotel = JsonConvert.DeserializeObject<Hotel>(dataObj.hotel.ToString());
            var posts = JsonConvert.DeserializeObject<List<Review>>(dataObj.posts.ToString());
            Reviews viewReviews = new Reviews { Hotel = hotel, Posts = posts, Review = new Review() };

            return View(viewReviews);
        }
        [HttpPost]
        public async Task<IActionResult> Review([FromForm] Review review, int id)
        {
            //string userGuid = _userManager.GetUserId(HttpContext.User);
            //_reviewService.AddReview(review, id, userGuid);
            await _clientService.AddReview(id, review);
            return RedirectToAction("Review", id);
        }
    }
}
