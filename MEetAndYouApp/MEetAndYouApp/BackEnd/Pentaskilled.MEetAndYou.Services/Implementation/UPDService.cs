using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pentaskilled.MEetAndYou.DataAccess.Contracts;
using Pentaskilled.MEetAndYou.Entities.DBModels;
using Pentaskilled.MEetAndYou.Entities.Models;
using Pentaskilled.MEetAndYou.Services.Contracts;

namespace Pentaskilled.MEetAndYou.Services.Implementation
{
    public class UPDService : IUPDService
    {

        private readonly IUPDDAO _updDAO;
        private readonly MEetAndYouDBContext _dbcontext;

        public UPDService(IUPDDAO updDAO, MEetAndYouDBContext dBContext)
        {
            _updDAO = updDAO;
            _dbcontext = dBContext;
        }


        public async Task<ItineraryResponse> GetItinerary(int userID)
        {
            if (userID > 0 )
            {
                ItineraryResponse getItinDbResult = await _updDAO.GetItineraryAsync(userID);
                return getItinDbResult;
            }
            return new ItineraryResponse("The itineraries could not be fetched from the service layer", false, null);
        }

        public async Task<NoteResponse> GetNote(int itineraryID)
        {
            // Input validation for the ID
            if (itineraryID > 0)
            {
                NoteResponse getNote = await _updDAO.GetNoteAsync(itineraryID);
                return getNote;
            }
            return new NoteResponse("The ratings could not be fetched from the service layer ", false, null);
        }

        public async Task<UserAccountRecordResponse> GetUser(int userID)
        {
            if (userID > 0)
            {
                UserAccountRecordResponse user = await _updDAO.getUserAccount(userID);
                return user;
            }

            return new UserAccountRecordResponse("failed", false, null);
        }

        public async Task<RatingResponse> GetUserRatings(int itineraryID)
        {
            if (itineraryID > 0)
            {
                RatingResponse getUserRatingsResult = await _updDAO.GetRatingsAsync(itineraryID);
                return getUserRatingsResult;
            }
            return new RatingResponse("The ratings could not be fetched from the service layer", false, null);
        }
    }
}
