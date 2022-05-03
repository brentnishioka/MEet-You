using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pentaskilled.MEetAndYou.DataAccess.Contracts;
using Pentaskilled.MEetAndYou.Entities.Models;

namespace Pentaskilled.MEetAndYou.DataAccess.Implementation
{
    public class SettingsDAO : ISettingsDAO
    {
        public Task<BaseResponse> updateUserEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse> updateUserPassword(string password)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse> updateUserPhone(string phone)
        {
            throw new NotImplementedException();
        }
    }
}
