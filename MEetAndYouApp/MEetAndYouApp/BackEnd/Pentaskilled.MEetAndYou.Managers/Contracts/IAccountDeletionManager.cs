using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pentaskilled.MEetAndYou.Entities.Models;

namespace Pentaskilled.MEetAndYou.Managers.Contracts
{
    public interface IAccountDeletionManager
    {
        public Task<BaseResponse> DeleteUser(int userID);
    }
}
