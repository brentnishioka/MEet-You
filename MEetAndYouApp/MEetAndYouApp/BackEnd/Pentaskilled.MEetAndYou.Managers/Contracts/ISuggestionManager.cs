using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pentaskilled.MEetAndYou.Entities.DBModels;
using Pentaskilled.MEetAndYou.Entities.Models;

namespace Pentaskilled.MEetAndYou.Managers.Contracts
{
    public interface ISuggestionManager
    {
        SuggestionResponse GetEvents(string category, string location, DateTime date);
        Task<SuggestionResponse> GetRandomEventsAsync();
        Task<BaseResponse> SaveEventAsync(List<Event> e, int itinID);
    }
}
