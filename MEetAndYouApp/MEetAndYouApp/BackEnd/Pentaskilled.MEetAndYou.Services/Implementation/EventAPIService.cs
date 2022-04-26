using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Pentaskilled.MEetAndYou.Services.Contracts;

namespace Pentaskilled.MEetAndYou.Services.Implementation
{
    public class EventAPIService : IAPIService
    {
        public Task<JObject> GetEventByCategoryAsync(string category, string location, DateTime date)
        {
            throw new NotImplementedException();
        }
    }
}
