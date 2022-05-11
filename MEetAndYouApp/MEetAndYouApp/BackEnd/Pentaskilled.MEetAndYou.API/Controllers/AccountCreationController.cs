using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pentaskilled.MEetAndYou.Entities.DBModels;
using System.Web.Http.Cors;
using System.Data;
using System.Data.SqlClient;
using Pentaskilled.MEetAndYou.DataAccess;
using Pentaskilled.MEetAndYou.Managers.Contracts;
using Pentaskilled.MEetAndYou.DataAccess.Contracts;
using Pentaskilled.MEetAndYou.Entities.Models;
using Pentaskilled.MEetAndYou.Managers;
namespace Pentaskilled.MEetAndYou.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AccountCreationController
    {
        private readonly MEetAndYouDBContext _dbcontext;
        private readonly AccountCreationManager _accountCreationManager;
        private readonly IAccountCreation _accountCreationDAO;

        public AccountCreationController(MEetAndYouDBContext context, AccountCreationManager accountCreationManager, IAccountCreation accountCreationDAO)
        {
            _dbcontext = context;
            this._accountCreationManager = accountCreationManager;
            this._accountCreationDAO = accountCreationDAO;
        }

        [HttpPost]
        [Route("/PostAccount")]
        public async Task<ActionResult<BaseResponse>> PostEvent(string email, string password, string phoneNumber)
        {
          
                BaseResponse result = await _accountCreationManager.BeginAccountCreation(email, password, phoneNumber);
                return result;
            
        }


    }
}
