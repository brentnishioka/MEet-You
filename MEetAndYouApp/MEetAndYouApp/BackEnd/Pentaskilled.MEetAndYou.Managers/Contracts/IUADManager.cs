using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pentaskilled.MEetAndYou.DataAccess;
using Pentaskilled.MEetAndYou.DataAccess.Implementation;
using Pentaskilled.MEetAndYou.Entities;
using Pentaskilled.MEetAndYou.Services.Contracts;
using Pentaskilled.MEetAndYou.Services.Implementation;

namespace Pentaskilled.MEetAndYou.Managers.Contracts
{
    public interface IUADManager
    {
        Task<List<int>> GetRegisteredAccountNumber();
        Task<List<int>> GetItineraryCount();

    }
}
