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

        public SuggestionManager(SuggestionDAO suggestionDAO, MEetAndYouDBContext dbContext)
        {
            _suggestionDAO = suggestionDAO;
            _dbContext = dbContext;
        }

        public SuggestionResponse GetEvents(string category, string location, DateTime date)
        {
            string successfulMessage = "Get Events was successful.";
            List<Event> eventList = new List<Event>();
            try
            {
                JObject result = _eventAPIService.GetEventByCategory(category, location, date);
                if (result != null)
                {
                    eventList = (List<Event>) _suggestionDAO.ParseJSON(result);
                }
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

        public async Task<BaseResponse> SaveEventAsync(List<Event> events)
        {
            BaseResponse response = await _suggestionDAO.SaveEventAsync(events);
            return response;
        }

        Task<SuggestionResponse> ISuggestionManager.GetRandomEventsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
