using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Pentaskilled.MEetAndYou.Services.Contracts;

namespace Pentaskilled.MEetAndYou.Services.Implementation
{
    public class EventAPIService : IAPIService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public EventAPIService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<JObject> GetEventByCategory(string category, string location, DateTime date)
        {
            HttpClient client = _httpClientFactory.CreateClient("EventService API");

            string requestURL = "URL" + category + location + date.ToString();
            
            //Send the GET request to yelp api
            var response = await client.GetAsync(requestURL);

            throw new NotImplementedException();
        }
    }
}
