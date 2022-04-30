using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Pentaskilled.MEetAndYou.DataAccess.Contracts;
using Pentaskilled.MEetAndYou.DataAccess.Implementation;
using Pentaskilled.MEetAndYou.Entities.DBModels;
using Pentaskilled.MEetAndYou.Entities.Models;
using Pentaskilled.MEetAndYou.Managers.Contracts;
using Pentaskilled.MEetAndYou.Services.Contracts;
using SerpApi;

namespace Pentaskilled.MEetAndYou.Managers.Implementation
{
    public class HyperlinkManager : IHyperlinkManager
    {
        private readonly MEetAndYouDBContext _dbContext;
        private readonly IHyperlinkDAO _hyperlinkDAO;

        public HyperlinkManager(IHyperlinkDAO hyperlinkDAO, MEetAndYouDBContext dbContext)
        {
            _hyperlinkDAO = hyperlinkDAO;
            _dbContext = dbContext;
        }

        public Task<HyperlinkResponse> AddUserToItineraryAsync(int userID, int itineraryID, string email, string permission)
        {
            throw new NotImplementedException();
        }

        public Task<HyperlinkResponse> RemoveUserToItineraryAsync(int userID, int itineraryID, string email, string permission)
        {
            throw new NotImplementedException();
        }
    }
}
