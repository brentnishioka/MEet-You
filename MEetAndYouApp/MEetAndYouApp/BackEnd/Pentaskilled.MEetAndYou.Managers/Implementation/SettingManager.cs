using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pentaskilled.MEetAndYou.Entities.DBModels;
using Pentaskilled.MEetAndYou.Entities.Models;
using Pentaskilled.MEetAndYou.Managers.Contracts;
using Pentaskilled.MEetAndYou.Services.Contracts;

namespace Pentaskilled.MEetAndYou.Managers.Implementation
{
    public class SettingManager : ISettingsManager
    {

        private readonly ISettingService settingService;
        private readonly MEetAndYouDBContext _dbcontext;

        public SettingManager(ISettingService sService, MEetAndYouDBContext context)
        {
            this.settingService = sService;
            this._dbcontext = context;
        }

        public async Task<BaseResponse> deleteUserAccount(int id)
        {
            if (id < 0)
                return new BaseResponse("User account was not able to be deleted.", false);
            return await settingService.deleteUserAccount(id);
        }

        public async Task<BaseResponse> disableUserAccount(int id)
        {
            if (id < 0)
                return new BaseResponse("User account was not able to be disabled", false);
            return await settingService.disableUserAccount(id);
        }

        public async Task<BaseResponse> enableUserAccount(int id)
        {
            if (id < 0)
                return new BaseResponse("User account was not able to be enabled", false);
            return await settingService.enableUserAccount(id);
        }

        public async Task<BaseResponse> updateUserEmail(int id, string email)
        {
            if (id < 0)
                return new BaseResponse("User email was not updated", false);
            return await settingService.updateUserEmail(id, email);
        }

        public async Task<BaseResponse> updateUserPassword(int id, string password)
        {
            if (id < 0)
                return new BaseResponse("User password was not updated", false);
            return await settingService.updateUserPassword(id, password);
        }

        public async Task<BaseResponse> updateUserPhone(int id, string phone)
        {
            if (id < 0)
                return new BaseResponse("User phone was not updated", false);
            return await settingService.updateUserPhone(id, phone);
        }
    }
}
