using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Pentaskilled.MEetAndYou.DataAccess.Contracts;
using Pentaskilled.MEetAndYou.DataAccess.Implementation;
using Pentaskilled.MEetAndYou.Entities.DBModels;
using Pentaskilled.MEetAndYou.Entities.Models;
using Pentaskilled.MEetAndYou.Managers.Contracts;
using Pentaskilled.MEetAndYou.Services.Contracts;
using SerpApi;

namespace Pentaskilled.MEetAndYou.Managers.Implementation
{
    public class SuggestionManager : ISuggestionManager
    {
        private readonly MEetAndYouDBContext _dbContext;
        private readonly ISuggestionDAO _suggestionDAO;
        private readonly IAPIService _eventAPIService;

        public SuggestionManager(ISuggestionDAO suggestionDAO, MEetAndYouDBContext dbContext, IAPIService eventAPIService)
        {
            _suggestionDAO = suggestionDAO;
            _dbContext = dbContext;
            _eventAPIService = eventAPIService;
        }

        /// <summary>
        /// Get a list of events base on the locaion, category and the date
        /// </summary>
        /// <param name="category"> the category of Events to be use to query</param>
        /// <param name="location"> the city of the Events </param>
        /// <param name="date"> the date of the wanted Events </param>
        /// <returns>  
        ///     Return a JObject that contains the JSON objects of events base on the input
        /// </returns>
        public async Task<SuggestionResponse> GetEvents(string category, string location, DateTime date)
        {
            string successfulMessage = "Get Events was successful.";
            List<Event> eventList = new List<Event>();
            try
            {
                //Check to see if user pass in a correct category
                bool isCategoryValid = await IsInCategory(category);
                if (!isCategoryValid)
                {
                    return new SuggestionResponse("Category is invalid", false, eventList);
                }

                // Run the Search for events
                JObject result = _eventAPIService.GetEventByCategory(category, location, date);
                if (result == null)
                {
                    return new SuggestionResponse("There was no events with that category", false, eventList);
                }
                eventList = (List<Event>)_suggestionDAO.ParseJSON(result);
            }
            catch (SerpApiSearchException ex)
            {
                return new SuggestionResponse
                    ("Getting Event from the SERP API failed \n" + ex.Message, false, eventList);
            }
            catch (Exception ex)
            {
                return new SuggestionResponse
                    ("Getting Event from SuggestionManager failed \n" + ex.Message, false, eventList);
            }
            return new SuggestionResponse(successfulMessage, true, eventList);
        }

        /// <summary>
        /// Add multiple Event objects to an existing Itinerary after checking to see if is owner
        /// </summary>
        /// <param name="events"> the list of events to be added to the itinerary </param>
        /// <param name="itinID"> the ID of the itinerary to store the Events </param>
        /// <param name="userID"> the ID of the itinerary's owner </param>
        /// <returns>  
        ///     A BaseResponse object that has the status of the operation and message. 
        /// </returns>
        public async Task<BaseResponse> SaveEventAsync(List<Event> events, int itinID, int userID)
        {
            BaseResponse response;
            try {
                // Check to see if the user own the itinerary
                BaseResponse isOwner = await _suggestionDAO.isUserOwner(userID, itinID);
                if (isOwner.IsSuccessful == false)
                {
                    return new BaseResponse("Not authorized to add Event", false);
                }

                response = await _suggestionDAO.SaveEventAsync(events, itinID);
            }

            catch(Exception ex)
            {
                return new BaseResponse("Saving event in Manager failed: \n" + ex.Message, false);
            }
            return response;
        }

        /// <summary>
        /// Generate a pseudo random category and fetch a List of Events from the API
        /// </summary>
        /// <returns>  
        ///     A SuggestionResponse object that has the status of the operation, message and the 
        ///     list of Events 
        /// </returns>
        public async Task<SuggestionResponse> GetRandomEventsAsync()
        {
            string successfulMessage = "Get Random Events was successful.";
            List<Event> eventList = new List<Event>();
            try
            {
                Category category = await GetRandomCategory();
                JObject result = _eventAPIService.GetEventByCategory(category.CategoryName);
                if (result == null)
                {
                    return new SuggestionResponse("There was no events with that category", false, eventList);
                }
                eventList = (List<Event>)_suggestionDAO.ParseJSON(result);
            }
            catch (SerpApiSearchException ex)
            {
                return new SuggestionResponse
                    ("Getting random Event from the SERP API failed \n" + ex.Message, false, eventList);
            }
            catch (Exception ex)
            {
                return new SuggestionResponse
                    ("Getting random Event from SuggestionManager failed \n" + ex.Message, false, eventList);
            }
            return new SuggestionResponse(successfulMessage, true, eventList);
        }

        /// <summary>
        /// Generate a random Category
        /// </summary>
        /// <returns>  
        ///     Return a random Category object
        /// </returns>
        public async Task<Category> GetRandomCategory()
        {
            Category category = await _suggestionDAO.GetRandomCategory();
            return category;
        }

        /// <summary>
        /// Check to see if the string category is valid with the data in the database. 
        /// </summary>
        /// <param name="category"> the string category to be validated </param>
        /// <returns>  
        ///     return true if category exist in the database
        ///     else return false if category does not exist in the database
        /// </returns>
        public async Task<bool> IsInCategory(string category)
        {
            CategoryResponse response = await _suggestionDAO.GetAllCategory();
            List<Category> categories = response.Data;
            foreach (Category c in categories)
            {
                string lowerCat = c.CategoryName.ToLower();
                if (lowerCat == category.ToLower())
                {
                    return true;
                }
            }
            return false;

        }

        /// <summary>
        /// Remove an Event from a user specific category after checking to see if the user is the owner
        /// </summary>
        /// <param name="eventID"> the ID of the Events to be removed from the itinerary </param>
        /// <param name="itinID"> the ID of the itinerary to remove the Event from </param>
        /// <param name="userID"> the ID of the itinerary's owner </param>
        /// <returns>  
        ///     A BaseResponse object that has the status of the operation and message. 
        /// </returns>
        public async Task<BaseResponse> DeleteEventAsync(int itinID, int eventID, int userID)
        {
            BaseResponse response;
            try
            {
                // Check to see if the user own the itinerary
                BaseResponse isOwner = await _suggestionDAO.isUserOwner(userID, itinID);
                if (isOwner.IsSuccessful == false)
                {
                    return new BaseResponse("Not authorized to add Event", false);
                }

                response = await _suggestionDAO.DeleteEventAsync(itinID, eventID);
            }

            catch (Exception ex)
            {
                return new BaseResponse("Delete event in Manager failed: \n" + ex.Message, false);
            }
            return response;
        }

        /// <summary>
        /// Add a new itinerary to the database
        /// </summary>
        /// <param name="itineraries"> the List of Itinerary objects to be added to the database </param>
        /// <returns>  
        ///     A BaseResponse object that has the status of the operation and message. 
        /// </returns>
        public async Task<BaseResponse> AddItineraryAsync(List<Itinerary> itineraries)
        {
            BaseResponse response;
            try
            {
                // Check to see if the user own the itinerary
                response = await _suggestionDAO.AddItineraryAsync(itineraries);
            }

            catch (Exception ex)
            {
                return new BaseResponse("Adding Itineraries in Manager failed: \n" + ex.Message, false);
            }
            return response;
        }

        /// <summary>
        /// Get a list of Itinerary own by a specific user using userID
        /// </summary>
        /// <param name="userID"> the ID of the user </param>
        /// <returns>  
        ///     A BaseResponse object that has the status of the operation and message. 
        /// </returns>
        public async Task<ItineraryResponse> GetUserItineraries(int userID)
        {
            ItineraryResponse response = await _suggestionDAO.GetUserItineraries(userID);
            if (response.IsSuccessful == true && response.Data.Count == 0)
            {
                response.Message = "The userID: " + userID + " have no itineraries.";
            }
            return response;
        }
    }
}
