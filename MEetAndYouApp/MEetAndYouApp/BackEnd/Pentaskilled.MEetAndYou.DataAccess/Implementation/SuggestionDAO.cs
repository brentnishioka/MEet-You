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


        /// <summary>
        /// Parse a JSON object and turn it into a collection of Event objects
        /// </summary>
        /// <param name="data"> the JSON object to be parse into objects</param>
        /// <param name="limit"> the amount of objects that will be parse into the collection, default at 10</param>
        /// <returns>  
        ///     A collection of Event objects with the specificy limit. 
        /// </returns>
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

        /// <summary>
        /// Add an Event object to the database in the Events table
        /// </summary>
        /// <param name="e"> the Event object to be saved into the database </param>
        /// <returns>  
        ///     A BaseResponse object that has the status of the operation and message. 
        /// </returns>
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

        /// <summary>
        /// Add multiple Event objects to an existing Itinerary. 
        /// </summary>
        /// <param name="events"> the list of events to be added to the itinerary </param>
        /// <param name="itinID"> the ID of the itinerary to store the Events </param>
        /// <returns>  
        ///     A BaseResponse object that has the status of the operation and message. 
        /// </returns>
        public async Task<BaseResponse> SaveEventAsync(List<Event> events, int itinID)
        {
            string message = "Saving Event failed.";
            bool isSuccessful = false;
            try
            {
                Itinerary itin = await _dbContext.Itineraries.FindAsync(itinID);
                if (itin == null)
                {
                    return new BaseResponse("Itinerary does not exist", isSuccessful);
                }
                // Save to Event table
                foreach (Event item in events)
                {
                    _dbContext.Entry(item).State = EntityState.Added;
                    // Save to the itinerary Object
                    itin.Events.Add(item);
                }

                int result = await _dbContext.SaveChangesAsync();

                //Check to see if all events are added successfully
                if (result == (events.Count * 2))
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

        /// <summary>
        /// Return a random Category object from the database 
        /// </summary>
        /// <returns>  
        ///     Return a Category object
        /// </returns>
        public async Task<Category> GetRandomCategory()
        {
            //return repo.Items.OrderBy(o => Guid.NewGuid()).First();
            Category randCategory = await _dbContext.Categories.OrderBy(o => Guid.NewGuid()).FirstAsync();
            return randCategory;
        }

        /// <summary>
        /// Get all the available Category in the database
        /// </summary>
        /// <returns>  
        ///     Returns a CategoryResponse object with status, message and a list of Category
        /// </returns>
        // Method to get all category for input check
        public async Task<CategoryResponse> GetAllCategory()
        {
            //return repo.Items.OrderBy(o => Guid.NewGuid()).First();
            List<Category> categories = null;
            string sucessMessage = "Getting all category was successful.";
            try
            {
                categories = await (from c in _dbContext.Categories
                                                   select c).ToListAsync<Category>();
            }
            catch (SqlException ex)
            {
                return new CategoryResponse
                    ("Getting category failed due to database error \n" + ex.Message, false, categories);
            }
            catch (Exception ex)
            {
                return new CategoryResponse("Getting category failed. \n" + ex.Message, false, categories);
            }
            return new CategoryResponse(sucessMessage, true, categories);
        }


        /// <summary>
        /// Convert a string represent a date to a DateTime object
        /// </summary>
        /// <param name="date"> the string that represent the date </param>
        /// <returns>  
        ///     Return a DateTime object parse from the string. 
        /// </returns>
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

        /// <summary>
        /// Check to see if the user owns a certain claim itinerary
        /// </summary>
        /// <param name="userID"> the user ID that make the claim </param>
        /// <param name="itinID"> the ID of the itinerary </param>
        /// <returns>  
        ///     A BaseResponse object that has the status of the operation and message. 
        /// </returns>
        public async Task<BaseResponse> isUserOwner(int userID, int itineraryID)
        {
            BaseResponse baseResponse;

            try
            {
                // Find associated itinerary
                Itinerary itin = await _dbContext.Itineraries.FindAsync(itineraryID);

                if (userID == itin.ItineraryOwner)
                {
                    baseResponse = new BaseResponse("User owns the itinerary", true);
                }
                else
                {
                   baseResponse = new BaseResponse("User does not own the itinerary", false);
                }

            }
            catch (SqlException ex)
            {
                return new BaseResponse("Could not find itinerary", false);
            }

            return baseResponse;
        }

        /// <summary>
        /// Remove an Event from a specific itinerary. 
        /// </summary>
        /// <param name="itinID"> the itinerary to remove the Event from </param>
        /// <param name="eventID"> the ID of the Event to be removed from the Iinerary </param>
        /// <returns>  
        ///     A BaseResponse object that has the status of the operation and message. 
        /// </returns>
        public async Task<BaseResponse> DeleteEventAsync(int itinID, int eventID)
        {
            string message = "Delete Event failed.";
            bool isSuccessful = false;
            try
            {
                // Find itinerary
                Itinerary itin = (from itinerary in _dbContext.Itineraries.Include("Events")
                                  where itinerary.ItineraryId == itinID
                                  select itinerary).FirstOrDefault<Itinerary>();

                //Find the Event
                Event e = await _dbContext.Events.FindAsync(eventID);
                //Console.Write("EventID: " + e.EventId + " " + "Event Name: " + e.EventName);
              

                //Remove event from the itinerary
                itin.Events.Remove(e);
                
                _dbContext.Entry(itin).State = EntityState.Modified;
                _dbContext.Entry(e).State = EntityState.Deleted;

                int result = await _dbContext.SaveChangesAsync();
                Console.WriteLine("Num rows affected: " + result);

                //Check to see if all events are added successfully
                if (result > 0)
                {
                    message = "Deleting Events was successful.";
                    isSuccessful = true;
                }

            }
            catch (SqlException ex)
            {
                return new BaseResponse
                    ("Delete event failed due to database error \n" + ex.Message, false);
            }
            catch (Exception ex)
            {
                return new BaseResponse("Delete event failed. \n" + ex.Message, false);
                //throw;
            }
            return new BaseResponse(message, isSuccessful);
        }

        /// <summary>
        /// Add a List of Itinerary to the dataabase. 
        /// </summary>
        /// <param name="itineraries"> the list of Itinerary to be added to the itinerary </param>
        /// <returns>  
        ///     A BaseResponse object that has the status of the operation and message. 
        /// </returns>
        public async Task<BaseResponse> AddItineraryAsync(List<Itinerary> itineraries)
        {
            string message = "Adding Itineraries failed.";
            bool isSuccessful = false;
            try
            {
                // Save to Event table
                foreach (Itinerary itinerary in itineraries)
                {
                    // Add the itinrary to the database by changing the state
                    _dbContext.Entry(itinerary).State = EntityState.Added;
                }

                int result = await _dbContext.SaveChangesAsync();

                //Check to see if all events are added successfully
                if (result == itineraries.Count)
                {
                    message = "Adding Itineraries was successful.";
                    isSuccessful = true;
                }

            }
            catch (SqlException ex)
            {
                // Remove Rolling back changes
                return new BaseResponse
                    ("Adding Itineraries failed due to database error \n" + ex.Message, false);
            }
            catch (Exception ex)
            {
                return new BaseResponse("Adding Itineraries failed. \n" + ex.Message, false);
            }
            return new BaseResponse(message, isSuccessful);
        }

        /// <summary>
        /// Get all Itineraries belongs to a user given userID
        /// </summary>
        /// <param name="userID"> the userID to get the Itinerary </param>
        /// <returns>  
        ///     A ItineraryResponse object that has the status of the operation, message and a list of Itinerary 
        /// </returns>
        public async Task<ItineraryResponse> GetUserItineraries(int userID)
        {
            List<Itinerary> itineraries = null;
            string sucessMessage = "Getting all Itineraries was successful.";
            try
            {
                itineraries = await (from itin in _dbContext.Itineraries.Include("Events")
                                     where itin.ItineraryOwner == userID
                                     select itin).ToListAsync<Itinerary>();
            }
            catch (SqlException ex)
            {
                return new ItineraryResponse
                    ("Getting Itineraries failed due to database error \n" + ex.Message, false, itineraries);
            }
            catch (Exception ex)
            {
                return new ItineraryResponse
                    ("Getting Itineraries failed. \n" + ex.Message, false, itineraries);
            }
            return new ItineraryResponse(sucessMessage, true, itineraries);
        }

        public async Task<BaseResponse> SaveEventsListAsync(List<Event> events)
        {
            string message = "Saving Event failed.";
            bool isSuccessful = false;
            try
            {
                // Save to Event table
                foreach (Event item in events)
                {
                    _dbContext.Entry(item).State = EntityState.Added;
                }

                int result = await _dbContext.SaveChangesAsync();

                //Check to see if all events are added successfully
                if (result == (events.Count))
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
    }
}
