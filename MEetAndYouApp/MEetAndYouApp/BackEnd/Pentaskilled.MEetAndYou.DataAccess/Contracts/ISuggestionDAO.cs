using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Pentaskilled.MEetAndYou.Entities.DBModels;

namespace Pentaskilled.MEetAndYou.DataAccess.Contracts
{
    public interface ISuggestionDAO
    {
        ICollection<Event> ParseJSON(JObject o);
    }
}
