using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pentaskilled.MEetAndYou.DataAccess.Contracts;
using Pentaskilled.MEetAndYou.Entities.Models;
using Pentaskilled.MEetAndYou.Services.Contracts;

namespace Pentaskilled.MEetAndYou.Services.Implementation
{
    public class SettingService : ISettingService
    {

        private readonly ISettingsDAO settingsDAO;

        public SettingService(ISettingsDAO settingDAO)
        {
            this.settingsDAO = settingDAO;
        }

        public async Task<BaseResponse> deleteUserAccount(int id)
        {
            if (id < 0)
                return new BaseResponse("User account was not able to be deleted.", false);
            
            return await settingsDAO.deleteUserAccount(id);
        }

        public async Task<BaseResponse> disableUserAccount(int id)
        {
            if (id < 0)
                return new BaseResponse("User account was not able to be disabled", false);

            return await settingsDAO.disableUserAccount(id);
        }

        public async Task<BaseResponse> enableUserAccount(int id)
        {
            if (id < 0)
                return new BaseResponse("User account was not able to be enabled", false);

            return await settingsDAO.enableUserAccount(id);
        }

        public async Task<BaseResponse> updateUserEmail(int id, string email)
        {
            if (id < 0)
                return new BaseResponse("User email was not able to be updated", false);

            return await settingsDAO.updateUserEmail(id, email);
        }

        public async Task<BaseResponse> updateUserPassword(int id, string password)
        {
            if (id < 0)
                return new BaseResponse("User password was not able to be updated", false);

            return await settingsDAO.updateUserPassword(id, password);
        }

        public async Task<BaseResponse> updateUserPhone(int id, string phone)
        {
            if (id < 0)
                return new BaseResponse("User phone was not able to be updated", false);

            return await settingsDAO.updateUserPhone(id, phone);
        }
    }
}
