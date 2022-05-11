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
        Task<BaseResponse> updateUserEmail(int id, string email);
        Task<BaseResponse> updateUserPhone(int id, string phone);
        Task<BaseResponse> updateUserPassword(int id, string password);


    }
}
