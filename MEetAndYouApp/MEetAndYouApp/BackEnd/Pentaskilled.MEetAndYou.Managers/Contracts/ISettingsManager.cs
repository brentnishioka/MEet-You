using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pentaskilled.MEetAndYou.Entities.Models;

namespace Pentaskilled.MEetAndYou.Managers.Contracts
{
    public interface ISettingsManager
    {
        Task<BaseResponse> updateUserEmail(int id, string email);
        Task<BaseResponse> updateUserPhone(int id, string phone);
        Task<BaseResponse> updateUserPassword(int id, string password);

        Task<BaseResponse> deleteUserAccount(int id);

        Task<BaseResponse> disableUserAccount(int id);

        Task<BaseResponse> enableUserAccount(int id);




    }
}
