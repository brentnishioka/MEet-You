using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Pentaskilled.MEetAndYou.Services.Contracts
{
    public interface IAPIService
    {
        JObject GetEventByCategory(string category, string location, DateTime date);
        JObject GetEventByCategory(string category);
    }
}
