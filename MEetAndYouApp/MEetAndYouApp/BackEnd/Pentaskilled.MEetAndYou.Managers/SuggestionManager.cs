using System;
using SerpApi;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Pentaskilled.MEetAndYou.Managers
{
    public class SuggestionManager
    {

        static void Main(string[] args)
        {
            
        }

        public JObject GetEventByCategory()
        {
            String apiKey = "";

            Hashtable ht = new Hashtable();
            ht.Add("engine", "google_events");
            ht.Add("q", "events in Long Beach");
            ht.Add("location", "Long Beach");

            try
            {
                GoogleSearch search = new GoogleSearch(ht, apiKey);
                JObject data = search.GetJson();
                JArray results = (JArray)data["events_results"];
                foreach (JObject result in results)
                {
                    Console.WriteLine("Found: " + result["title"]);
                }

                return data;
            }
            catch (SerpApiSearchException ex)
            {
                Console.WriteLine("Exception:");
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
    }
}