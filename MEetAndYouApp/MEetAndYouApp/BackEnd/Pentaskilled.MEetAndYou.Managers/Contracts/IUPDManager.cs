using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pentaskilled.MEetAndYou.Entities.Models;

namespace Pentaskilled.MEetAndYou.Managers.Contracts
{
    public interface IUPDManager
    {

        Task<UPDataResponse> GetUPData(int userID);




    }
}
