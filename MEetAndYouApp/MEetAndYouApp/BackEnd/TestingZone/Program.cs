using System.Collections;
using System.Globalization;
using Newtonsoft.Json.Linq;
using Pentaskilled.MEetAndYou.DataAccess;
using Pentaskilled.MEetAndYou.DataAccess.Implementation;
using Pentaskilled.MEetAndYou.Entities.DBModels;
using Pentaskilled.MEetAndYou.Entities.Models;
using Pentaskilled.MEetAndYou.Managers;
using Pentaskilled.MEetAndYou.Services.Implementation;
using SerpApi;

namespace Pentaskilled.MEetAndYou.TestingZone;
public class Program
{
    //public static void Main()
    //{
    //    //Console.WriteLine("THis is the current datetime: ");
    //    //Console.WriteLine(DateTime.UtcNow.ToString());

    //    //AuthnDAO authnDAO = new AuthnDAO();
    //    //AuthorizationDAO authzDAO = new AuthorizationDAO(); 
    //    //AuthnService authnService = new AuthnService();
    //    //AuthorizationManager authzController = new AuthorizationManager();
    //    //CopyItineraryDAO copyItinDAO = new CopyItineraryDAO();

    //    // Calling the method to save token to the databse, Brent ID
    //    //string token = "blueberrystrawberryy";
    //    //string brokenToken = "blu123rrystraw456ryy";
    //    //int userID = 4;
    //    //string claimedRole = "Admin";

    //    //bool result = authnDAO.SaveToken(userID, token).Result;
    //    //Console.WriteLine(result);

    //    //Console.WriteLine("");
    //    //Console.WriteLine("Verifying token: ");
    //    //bool isVerified = authzDAO.VerifyToken(userID, token);
    //    //Console.WriteLine("Result: " + isVerified);

    //    //Removing the token after verfication
    //    //Console.WriteLine("");
    //    //Console.WriteLine("Start removing token: ");
    //    //bool isDeleted = authnDAO.DeleteToken(userID).Result;
    //    //Console.WriteLine("Result: " + isDeleted);

    //    // Testing the Authorization Manager
    //    //Console.WriteLine("");
    //    //Console.WriteLine("Start verifying claimed role: ");
    //    //bool actualValue = authzController.IsAuthorized(userID, token, claimedRole);
    //    //Console.WriteLine("Result: " + actualValue);

    //    // Testing Itinerary DAO
    //    //Console.WriteLine("Before getting itin");
    //    //int itineraryID = 7;
    //    //Itinerary itinerary = copyItinDAO.GetItinerary(itineraryID).Result;
    //    //if (itinerary != null)
    //    //{
    //    //    Console.Write("Resulting itinerary: ");
    //    //    Console.WriteLine("ItinName: " + itinerary.ItineraryName);
    //    //}

    //    String apiKey = "";
    //    Hashtable ht = new Hashtable();
    //    ht.Add("engine", "google_events");
    //    ht.Add("q", "events in Long Beach");
    //    ht.Add("location", "Long Beach");

    //    try
    //    {
    //        GoogleSearch search = new GoogleSearch(ht, apiKey);
    //        JObject data = search.GetJson();
    //        JArray results = (JArray)data["organic_results"];
    //        foreach (JObject result in results)
    //        {
    //            Console.WriteLine("Found: " + result["title"]);
    //        }
    //    }
    //    catch(SerpApiSearchException ex)
    //    {
    //        Console.WriteLine("Exception:");
    //        Console.WriteLine(ex.ToString());
    //    }
    //}

    static void Main(string[] args)
    {
        // secret api key from https://serpapi.com/dashboard
        //String apiKey = "";

        //Hashtable ht = new Hashtable();
        //ht.Add("engine", "google_events");
        //ht.Add("q", "events in Long Beach");
        //ht.Add("location", "Long Beach");

        //try
        //{
        //    GoogleSearch search = new GoogleSearch(ht, apiKey);
        //    JObject data = search.GetJson();
        //    JArray results = (JArray)data["events_results"];
        //    foreach (JObject result in results)
        //    {
        //        Console.WriteLine("Found: " + result["title"]);
        //    }
        //}
        //catch (SerpApiSearchException ex)
        //{
        //    Console.WriteLine("Exception:");
        //    Console.WriteLine(ex.ToString());
        //}

        //Test the API and conversion
        //string location = "Long Beach";
        //string category = "coffee";
        //Console.WriteLine("Parsing the date: ");
        //string date = "May 1";
        //DateTime dateTime = DateConversion(date);
        //Console.WriteLine(date.ToString());
        //int limit = 10;

        //EventAPIService eventAPI = new EventAPIService();
        //JObject results = eventAPI.GetEventByCategory(category, location, dateTime);
        //SuggestionDAO suggestionDAO = new SuggestionDAO();
        //ICollection<Event> eventList = suggestionDAO.ParseJSON(results, limit);

        //foreach(Event e in eventList)
        //{
        //    Console.WriteLine(e.EventName.ToString());
        //    Console.WriteLine(e.Description.ToString());
        //    Console.WriteLine(e.EventDate.ToString());
        //    Console.WriteLine(e.CategoryNames.ToString());
        //    Console.WriteLine(e.Address);
        //    Console.WriteLine("--------------------------------------");
        //}

        //Test Saving events to DB
        SuggestionDAO suggestionDAO = new SuggestionDAO();
        List<Event> eventList = new List<Event>();
        int itinID = 4;
        int numEvent = 3;

        for (int i = 0; i < numEvent; i++)
        {
            Event temp = new Event {
                EventName = "Test event " + i,
                Address = i + "Main street, Long Beach CA 99284",
                Description = "Test events use for saving events unit test",
                EventDate = DateTime.Now
            };
            eventList.Add(temp);
        }

        //Act
        Console.WriteLine("Saving Events");
        BaseResponse response = suggestionDAO.SaveEventAsync(eventList, itinID).Result;
        Console.WriteLine("Saving events Successful");
        Console.WriteLine(response.Message);
    }

    public static DateTime DateConversion(string date)
    {
        CultureInfo ci = new CultureInfo("en-US");
        return DateTime.Parse(date, ci);
    }
}