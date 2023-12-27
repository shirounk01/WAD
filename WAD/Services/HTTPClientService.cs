using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Web;
using WAD.Models;
using WAD.Models.DTOs;
using WAD.Services.Interfaces;
using WAD.ViewModels;

namespace WAD.Services
{
    public class HTTPClientService : IHTTPClientService
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _config;
        private readonly string _endpointBase;
        private readonly IHttpContextAccessor _context;

        public HTTPClientService(IConfiguration config, IHttpContextAccessor context)
        {
            _client = new HttpClient();
            _config = config;
            _endpointBase = _config["API:Local"];
            _context = context;
        }

        public async Task<List<Hotel>> GetHotelsByModel(Hotel hotel)
        {
            var res = await _client.PostAsJsonAsync(_endpointBase + "Hotel", hotel);
            var content = await res.Content.ReadAsStringAsync();
            var dataObj = JsonConvert.DeserializeObject<List<Hotel>>(content);
            return dataObj;
        }

        public async Task<List<Hotel>> FilterHotels(Filter filter, List<Hotel> hotels)
        {
            var queryString = ToQueryString(filter);
            var res = await _client.PostAsJsonAsync(_endpointBase + "Hotel/Filter" + queryString, hotels);
            var content = await res.Content.ReadAsStringAsync();
            var dataObj = JsonConvert.DeserializeObject<List<Hotel>>(content);
            return dataObj;
        }

        static string ToQueryString(object obj)
        {
            // Use System.Web.HttpUtility to convert object properties to a query string
            string queryString = string.Join("&",
                obj.GetType().GetProperties()
                   .Select(property => $"{property.Name}={HttpUtility.UrlEncode(property.GetValue(obj)?.ToString() ?? "")}"));
            queryString = queryString.Replace("None", "0");

            return "?" + queryString;
        }

        public async Task<dynamic> GetFlightsByInfo(FlightInfo flightInfo)
        {
            var res = await _client.PostAsJsonAsync(_endpointBase + "Flight", flightInfo);
            var content = await res.Content.ReadAsStringAsync();
            var dataObj = JsonConvert.DeserializeObject<dynamic>(content);
            return dataObj;
        }

        public async Task<List<FlightPack>> FilterFlights(Filter filter, List<FlightPack> flights)
        {
            var queryString = ToQueryString(filter);
            var res = await _client.PostAsJsonAsync(_endpointBase + "Flight/Filter" + queryString, flights);
            var content = await res.Content.ReadAsStringAsync();
            var dataObj = JsonConvert.DeserializeObject<List<FlightPack>>(content);
            return dataObj;
        }

        public async Task<int> GetRates(string currency)
        {
            var res = await _client.GetAsync(string.Format(_config["API:Currency"], currency));
            var content = await res.Content.ReadAsStringAsync();
            var json = JsonConvert.DeserializeObject(content).ToString();

            JObject jsonObj = JObject.Parse((string)json);
            JObject ratesObj = (JObject)jsonObj["rates"];

            var dataObj = (int)((double)ratesObj[currency] * 100);


            return dataObj;
        }

        public async Task<string> Login(UserInfo userInfo)
        {
            var res = await _client.PostAsJsonAsync(_endpointBase + "User/Login", userInfo);
            var content = await res.Content.ReadAsStringAsync();

            return content;
        }


        public async Task BookHotel(int id, Hotel hotel)
        {
            var hotelInfo = new { OpenDate = hotel.OpenDate, CloseDate = hotel.CloseDate };
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _context.HttpContext.Session.GetString("token"));

            var res = await _client.PostAsJsonAsync(_endpointBase + $"Hotel/Book/{id}", hotelInfo);
            return;
        }

        public async Task<dynamic> Review(int id)
        {
            var res = await _client.GetAsync(_endpointBase + $"Hotel/Reviews/{id}");
            var content = await res.Content.ReadAsStringAsync();
            var dataObj = JsonConvert.DeserializeObject<dynamic>(content);
            return dataObj;

        }

        public async Task AddReview(int id, Review review)
        {
            var reviewInfo = new { Rating = review.Rating, Comment = review.Comment, Created = review.Created };
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _context.HttpContext.Session.GetString("token"));
            var res = await _client.PostAsJsonAsync(_endpointBase + $"Hotel/Reviews/Post/{id}", reviewInfo);
            var content = await res.Content.ReadAsStringAsync();
            return;
        }

        public async Task BookFlight(int goingId, int comingId)
        {
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _context.HttpContext.Session.GetString("token"));
            var res = await _client.PostAsync(_endpointBase + $"Flight/Book/{goingId}-{comingId}", null);
            return;
        }

        public async Task<History> GetHistory()
        {
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _context.HttpContext.Session.GetString("token"));
            var res = await _client.GetAsync(_endpointBase + $"User/Profile");

            var content = await res.Content.ReadAsStringAsync();
            var dataObj = JsonConvert.DeserializeObject<History>(content);
            return dataObj;
        }

        public async Task<string> Register(UserInfo userInfo)
        {
            var res = await _client.PostAsJsonAsync(_endpointBase + $"User/Register", userInfo);
            var content = await res.Content.ReadAsStringAsync();
            return content;
        }
    }
}
