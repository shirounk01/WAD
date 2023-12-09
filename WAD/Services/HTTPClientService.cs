using Newtonsoft.Json;
using System.Web;
using WAD.Models;
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
    }
}
