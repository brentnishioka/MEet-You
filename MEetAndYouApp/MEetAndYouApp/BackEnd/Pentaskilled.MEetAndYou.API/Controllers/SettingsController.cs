using Microsoft.AspNetCore.Mvc;
using Pentaskilled.MEetAndYou.DataAccess.Contracts;
using Pentaskilled.MEetAndYou.DataAccess.Implementation;
using Pentaskilled.MEetAndYou.Entities.DBModels;
using Pentaskilled.MEetAndYou.Entities.Models;
using Pentaskilled.MEetAndYou.Managers;
using Pentaskilled.MEetAndYou.Managers.Contracts;
using Pentaskilled.MEetAndYou.Managers.Implementation;
using Pentaskilled.MEetAndYou.Services.Contracts;

namespace Pentaskilled.MEetAndYou.API.Controllers
{
    public class SettingsController : Controller
    {
        private readonly ISettingsManager _settingsManager;
        private readonly MEetAndYouDBContext _dbContext;

        public SettingsController(ISettingsManager settingsManager, MEetAndYouDBContext context)
        {
            _settingsManager = settingsManager;
            _dbContext = context;
        }


        [HttpPost]
        [Route("/DeleteUserAccount")]
        public async Task<ActionResult<BaseResponse>> DeleteUserAccount(int userId)
        {
            if (userId < 0) 
                return new BaseResponse("User account was not able to be deleted in controller", false);
            return await _settingsManager.deleteUserAccount(userId);
        }

        [HttpPost]
        [Route("/DisableUserAccount")]
        public async Task<ActionResult<BaseResponse>> DisableUserAccount(int userId)
        {
            if (userId < 0)
                return new BaseResponse("User account was not able to be disabled in controller", false);
            return await _settingsManager.disableUserAccount(userId);
        }

        [HttpPost]
        [Route("/EnableUserAccount")]
        public async Task<ActionResult<BaseResponse>> EnableUserAccount(int userId)
        {
            if (userId < 0)
                return new BaseResponse("User account was not able to be enabled in controller", false);
            return await _settingsManager.enableUserAccount(userId);
        }

        [HttpPost]
        [Route("/UpdateUserEmail")]
        public async Task<ActionResult<BaseResponse>> UpdateUserEmail(int userId, string email)
        {
            if (userId < 0)
                return new BaseResponse("User email was not updated in controller", false);
            return await _settingsManager.updateUserEmail(userId, email);
        }

        [HttpPost]
        [Route("/UpdateUserPassword")]
        public async Task<ActionResult<BaseResponse>> UpdateUserPassword(int userId, string password)
        {
            if (userId < 0)
                return new BaseResponse("User password was not updated in controller", false);
            return await _settingsManager.updateUserPassword(userId, password);
        }

        [HttpPost]
        [Route("/UpdateUserPhone")]
        public async Task<ActionResult<BaseResponse>> UpdateUserPhone(int userId, string phone)
        {
            if (userId < 0)
                return new BaseResponse("User phone was not updated in controller", false);
            return await _settingsManager.updateUserPhone(userId, phone);
        }


    }
}
