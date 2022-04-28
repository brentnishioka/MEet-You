using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Pentaskilled.MEetAndYou.DataAccess.Contracts;
using Pentaskilled.MEetAndYou.Entities.DBModels;
using Pentaskilled.MEetAndYou.Entities.Models;

namespace Pentaskilled.MEetAndYou.DataAccess.Implementation
{
    public class SuggestionDAO : ISuggestionDAO
    {
        private readonly MEetAndYouDBContext _dbContext;

        public SuggestionDAO()
        {
            _dbContext = new MEetAndYouDBContext();
        }
        public SuggestionDAO(MEetAndYouDBContext dbContext)
        {
            _dbContext = dbContext;
        }


        public  ICollection<Event> ParseJSON(JObject data, int limit = 10)
        {
            //Get the category of the events
            var parameter = data["search_parameters"];
            string category = parameter["q"].ToString();

            JArray results = (JArray)data["events_results"];
            ICollection<Event> eventList = new List<Event>();

            //Create a list of Events
            int counter = 0;
            foreach (var result in results)
            {
                string eventName = result["title"].ToString();
                string description = "";

                if (result["description"] != null) {
                    description = result["description"].ToString();
                }
                string eventAddress = this.addressConcatenation(result["address"]);
                string date = (result["date"])["start_date"].ToString();

                //Convert the date into Datetime object
                DateTime eventDate = this.DateConversion(date);

                Event temp = new Event {
                    EventName = eventName,
                    Address = eventAddress,
                    Description = description,
                    EventDate = eventDate
                };
                temp.CategoryNames.Add(new Category { CategoryName = category });
                eventList.Add(temp);

                //Create a limit for the Event list to 10, by returning the list 
                counter++;
                if (counter >= limit)
                {
                    return eventList;
                }
            }
            return eventList;
        }

        public async Task<BaseResponse> SaveEvent(Event e)
        {
            string sucessMessage = "Saving Event was successful.";
            try
            {
                _dbContext.Entry(e).State = EntityState.Added;
                int result = await _dbContext.SaveChangesAsync();

            }
            catch (SqlException ex)
            {
                return new BaseResponse
                    ("Saving event failed due to database error \n" + ex.Message, false);
            }
            catch (Exception ex)
            {
                return new BaseResponse("Saving event failed. \n" + ex.Message, false);
            }
            return new BaseResponse(sucessMessage, true);
        }

        public async Task<BaseResponse> SaveEventAsync(List<Event> events)
        {
            string message = "Saving Event failed.";
            bool isSuccessful = false;
            try
            {
                foreach (Event item in events)
                {
                    _dbContext.Entry(item).State = EntityState.Added;
                }
                int result = await _dbContext.SaveChangesAsync();

                //Check to see if all events are added successfully
                if (result == events.Count)
                {
                    message = "Saving Events was successful.";
                    isSuccessful = true;
                }

            }
            catch (SqlException ex)
            {
                // Remove Rolling back changes
                return new BaseResponse
                    ("Saving event failed due to database error \n" + ex.Message, false);
            }
            catch (Exception ex)
            {
                return new BaseResponse("Saving event failed. \n" + ex.Message, false);
            }
            return new BaseResponse(message, isSuccessful);
        }

        // Method to get a random category
        public async Task<Category> GetRandomCategory()
        {
            //return repo.Items.OrderBy(o => Guid.NewGuid()).First();
            Category randCategory = await _dbContext.Categories.OrderBy(o => Guid.NewGuid()).FirstAsync();
            return randCategory;
        }

        public DateTime DateConversion(string date)
        {
            CultureInfo ci = new CultureInfo("en-US");
            return DateTime.Parse(date, ci);
        }

        public string addressConcatenation (JToken addresses)
        {
            string result = "";
            foreach (JToken address in addresses)
            {
                result = result + address.ToString() + " ";
            }
            return result;
        }
    }
}
