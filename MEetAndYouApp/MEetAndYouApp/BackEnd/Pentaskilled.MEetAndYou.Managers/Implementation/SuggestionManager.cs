using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pentaskilled.MEetAndYou.DataAccess.Implementation;
using Pentaskilled.MEetAndYou.Entities.DBModels;
using Pentaskilled.MEetAndYou.Entities.Models;
using Pentaskilled.MEetAndYou.Managers.Contracts;

namespace Pentaskilled.MEetAndYou.Managers.Implementation
{
    public class SuggestionManager : ISuggestionManager
    {
        private readonly MEetAndYouDBContext _dbContext;
        private readonly SuggestionDAO _copyItineraryDAO;

        public SuggestionManager(SuggestionDAO suggestionDAO, MEetAndYouDBContext dbContext)
        {
            _copyItineraryDAO = suggestionDAO;
            _dbContext = dbContext;
        }

        public ICollection<Event> GetEventsAsync(string category, string location, DateTime date)
        {
            throw new NotImplementedException();
        }

        public ICollection<Event> GetRandomEventsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse> SaveEventAsync(Event e)
        {
            throw new NotImplementedException();
        }
    }
}
