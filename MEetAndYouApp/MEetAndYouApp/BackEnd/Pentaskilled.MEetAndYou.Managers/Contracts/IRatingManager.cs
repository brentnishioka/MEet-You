using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pentaskilled.MEetAndYou.Entities.Models;

namespace Pentaskilled.MEetAndYou.Managers.Contracts
{
    public interface IRatingManager
    {
        Task<ItineraryResponse> RetrieveUserItinerary(int userID, int itineraryID);
        Task<RatingResponse> RetrieveUserRatings(int itineraryID);
        Task<BaseResponse> CreateRating(int eventID, int itineraryID, int userRating);
        Task<BaseResponse> ModifyRating(int eventID, int itineraryID, int userRating);
        Task<BaseResponse> CreateItineraryNote(int itineraryID, string noteContent);
        Task<BaseResponse> ModifyItineraryNote(int itineraryID, string noteContent);
    }
}
