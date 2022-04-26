using System;
using System.Collections.Generic;
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


        public async Task<ICollection<Event>> ParseJSONAsync(JObject o)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponse> SaveEvent(Event e)
        {
            throw new NotImplementedException();
        }
    }
}
