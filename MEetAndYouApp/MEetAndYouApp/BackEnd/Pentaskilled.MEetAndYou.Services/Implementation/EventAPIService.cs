using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Pentaskilled.MEetAndYou.Services.Contracts;
using SerpApi;

namespace Pentaskilled.MEetAndYou.Services.Implementation
{
    public class EventAPIService : IAPIService
    {
        private readonly IConfiguration _configuration;
        private readonly string _eventsAPIkey;

        public EventAPIService()
        {

        }

        public EventAPIService(IConfiguration configuration)
        {
            _configuration = configuration;
            _eventsAPIkey = _configuration["EventsAPI"];
        }

        public JObject GetEventByCategoryAsync(string category, string location, DateTime date)
        {
            String apiKey = "";
            JObject result = null;

            Hashtable ht = new Hashtable();
            ht.Add("engine", "google_events");
            ht.Add("q", category);
            ht.Add("location", location);
            ht.Add("date", date.ToString());

            try
            {
                GoogleSearch search = new GoogleSearch(ht, apiKey);
                JObject data = search.GetJson();
                result = data;
            }
            catch (SerpApiSearchException ex)
            {
                Console.WriteLine("Exception:");
                Console.WriteLine(ex.ToString());
                return result;
            }
            return result;
        }
    }
}
