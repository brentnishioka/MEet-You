using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pentaskilled.MEetAndYou.Entities.DBModels;
using Pentaskilled.MEetAndYou.Entities.Models;

namespace Pentaskilled.MEetAndYou.DataAccess.Contracts
{
    public interface IRatingDAO
    {
        Task<ItineraryResponse> GetUserItinerary(int userID, int itineraryID);
        Task<BaseResponse> AddRatingInDBAsync(UserEventRating userRating);
        Task<BaseResponse> ModifyRatingInDBAsync(UserEventRating userRating);
        Task<BaseResponse> AddNoteInDBAsync(ItineraryNote itineraryNote);
        Task<BaseResponse> ModifyNoteInDBAsync(ItineraryNote itineraryNote);
    }
}
