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

        public async Task<Category> GetRandomCategory()
        {
            Category category = await _suggestionDAO.GetRandomCategory();
            return category;
        }

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
