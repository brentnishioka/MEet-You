using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pentaskilled.MEetAndYou.Entities.DBModels;

namespace Pentaskilled.MEetAndYou.DataAccess.Contracts
{
    public interface IUserDAO
    {

        public Task<UserAccountRecord> getUserAccount(int userID);





    }
}
