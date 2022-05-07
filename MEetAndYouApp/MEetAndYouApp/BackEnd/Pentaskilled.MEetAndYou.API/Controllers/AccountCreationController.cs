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


namespace Pentaskilled.MEetAndYou.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AccountCreationController
    {
        private readonly MEetAndYouDBContext _dbcontext;
        private readonly IAccountCreationManager _memoryAlbumManager;
        private readonly IMemoryAlbumDAO _memoryAlbumDAO;

        public AccountCreationController(MEetAndYouDBContext context, IMemoryAlbumManager memoryAlbumManager, IMemoryAlbumDAO memoryAlbumDAO)
        {
            _dbcontext = context;
            this._memoryAlbumManager = memoryAlbumManager;
            this._memoryAlbumDAO = memoryAlbumDAO;
        }
    }
}
