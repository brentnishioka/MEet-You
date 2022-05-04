using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pentaskilled.MEetAndYou.Entities.DBModels;
using Pentaskilled.MEetAndYou.Entities.Models;
using Pentaskilled.MEetAndYou.DataAccess.Contracts;
namespace Pentaskilled.MEetAndYou.DataAccess.Implementation
{
    public class ItineraryDAO : IItineraryDAO
    {
        private MEetAndYouDBContext _dbContext;


        public ItineraryDAO()
        {
            _dbContext = new MEetAndYouDBContext();
        }

        public ItineraryDAO(MEetAndYouDBContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public Task<BaseResponse> ChangeItineraryName(string name)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse> ChangeItineraryRating(int itineraryID)
        {
            throw new NotImplementedException();
        }

        public async Task<ItineraryResponse> GetUserItineraries(int userID)
        {
            List<Itinerary> itineraries = null;
            string sucessMessage = "Getting all Itineraries was successful.";
            try
            {
                itineraries = await (from itin in _dbContext.Itineraries.Include("Events")
                                     where itin.ItineraryOwner == userID
                                     select itin).ToListAsync<Itinerary>();
            }
            catch (SqlException ex)
            {
                return new ItineraryResponse
                    ("Getting Itineraries failed due to database error \n" + ex.Message, false, itineraries);
            }
            catch (Exception ex)
            {
                return new ItineraryResponse
                    ("Getting Itineraries failed. \n" + ex.Message, false, itineraries);
            }
            return new ItineraryResponse(sucessMessage, true, itineraries);
        }

    }
}
