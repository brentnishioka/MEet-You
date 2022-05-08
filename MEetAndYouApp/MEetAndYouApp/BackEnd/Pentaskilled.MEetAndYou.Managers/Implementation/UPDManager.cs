using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pentaskilled.MEetAndYou.Entities.Models;
using Pentaskilled.MEetAndYou.Entities.DBModels;
using Pentaskilled.MEetAndYou.DataAccess.Implementation;
using Microsoft.EntityFrameworkCore;
using Pentaskilled.MEetAndYou.Managers.Contracts;
using Pentaskilled.MEetAndYou.DataAccess.Contracts;
using Pentaskilled.MEetAndYou.Services.Contracts;

namespace Pentaskilled.MEetAndYou.Managers.Implementation
{
    public class UPDManager : IUPDManager
    {
        private readonly IUPDService _updService;
        private readonly MEetAndYouDBContext _dbcontext;


        public UPDManager(IUPDService updservice, MEetAndYouDBContext dbcontext)
        {
            this._updService = updservice;
            this._dbcontext = dbcontext;
        }

        /* Method to get that the list of itineraries associated with the user along with the 
           account information, both pieces of data are wrapped ina a UPData object.*/
        public async Task<UPDataResponse> GetUPData(int userID)
        {
            // TODO: make it so the UPDataResponse includes things like the rating of itinerary, 
            if (userID > 0)
            {
                ItineraryResponse iResponse = await _updService.GetItinerary(userID);
                UserAccountRecordResponse uResponse = await _updService.GetUser(userID);
                UPDataResponse upDataResponse = new UPDataResponse("UPD data gathered successfully", true, uResponse, iResponse.Data);
                return upDataResponse;
            }
            return new UPDataResponse("UPD data gathering failed manager layer", false, null, null);
           
        }

    }
}
