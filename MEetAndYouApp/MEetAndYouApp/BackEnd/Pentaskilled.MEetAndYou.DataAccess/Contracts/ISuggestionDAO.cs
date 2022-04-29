using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Pentaskilled.MEetAndYou.Entities.DBModels;
using Pentaskilled.MEetAndYou.Entities.Models;

namespace Pentaskilled.MEetAndYou.DataAccess.Contracts
{
    public interface ISuggestionDAO
    {
        ICollection<Event> ParseJSON(JObject o, int limit = 10);
        Task<BaseResponse> SaveEvent(Event e);
        Task<BaseResponse> SaveEventAsync(List<Event> e, int itinID);
        Task<Category> GetRandomCategory();
        Task<CategoryResponse> GetAllCategory();
    }
}
