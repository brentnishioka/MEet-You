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
        ICollection<Event> GetEvents(string category, string location, DateTime date);
        ICollection<Event> GetRandomEvents();
        BaseResponse SaveEvent(Event e);
    }
}
