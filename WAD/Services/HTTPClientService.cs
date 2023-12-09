using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Web;
using WAD.Models;
using WAD.Models.DTOs;
using WAD.Services.Interfaces;

namespace WAD.Services
{
    public class HTTPClientService : IHTTPClientService
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _config;
        private readonly string _endpointBase;

        public HTTPClientService(IConfiguration config)
        {
            _client = new HttpClient();
            _config = config;
            _endpointBase = _config["API:Local"];
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
    }
}
