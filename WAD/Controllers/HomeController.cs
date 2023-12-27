using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WAD.Models;

namespace WAD.Controllers
{
    public class HomeController : Controller
    {
        readonly IConfiguration _config;

        public HomeController(IConfiguration config)
        {
            _config = config;
        }

        public IActionResult Index()
        {
            Console.WriteLine(HttpContext.Session.GetString("token"));
            return View();
        }
        [HttpPost]
        public IActionResult Index([FromForm] Flight flight, [FromForm] Hotel hotel, string search)
        {
            if (search == "flight")
            {
                return RedirectToAction("Index", "Flight", flight);
            }
            if(search == "hotel")
            {
                return RedirectToAction("Index", "Hotel", hotel);
            }
            return View("Error");
        }

        public IActionResult Confirmation()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}