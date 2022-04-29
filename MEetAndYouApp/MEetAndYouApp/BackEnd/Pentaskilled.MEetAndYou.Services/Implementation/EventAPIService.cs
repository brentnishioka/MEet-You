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
        //private readonly string _eventsAPIkey = "5ebf47bd63dbb46ff6dcc84edbc58cb326723d49af7dedff19b243b94e3ab4b8";
        private readonly string _eventsAPIkey;

        public EventAPIService()
        {

        }

        public EventAPIService(string apiKey)
        {
            _eventsAPIkey = apiKey;
        }

        public EventAPIService(IConfiguration configuration)
        {
            _configuration = configuration;
            _eventsAPIkey = _configuration["EventsAPI:ServiceApiKey"];
        }

        public JObject GetEventByCategory(string category, string location, DateTime date)
        {
            String apiKey = _eventsAPIkey;
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

        //To use to get events of random category
        public JObject GetEventByCategory(string category)
        {
            String apiKey = _eventsAPIkey;
            JObject result = null;

            Hashtable ht = new Hashtable();
            ht.Add("engine", "google_events");
            ht.Add("q", category);

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
