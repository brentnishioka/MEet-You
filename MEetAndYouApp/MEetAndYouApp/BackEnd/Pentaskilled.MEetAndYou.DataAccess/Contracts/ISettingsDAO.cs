using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pentaskilled.MEetAndYou.Entities.Models;

namespace Pentaskilled.MEetAndYou.DataAccess.Contracts
{
    public interface ISettingsDAO
    { 
        Task<BaseResponse> updateUserEmail(string email);
        Task<BaseResponse> updateUserPhone(string phone);
        Task<BaseResponse> updateUserPassword(string password);


    }
}
