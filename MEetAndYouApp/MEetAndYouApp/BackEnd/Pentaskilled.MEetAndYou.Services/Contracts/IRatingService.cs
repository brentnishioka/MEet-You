using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pentaskilled.MEetAndYou.Entities.DBModels;
using Pentaskilled.MEetAndYou.Entities.Models;

namespace Pentaskilled.MEetAndYou.Services.Contracts
{
    public interface IRatingService
    {
        Task<ItineraryResponse> GetItineraryService(int userID, int itineraryID);
        Task<RatingResponse> GetUserRatingsService(int itineraryID);
        Task<NoteResponse> GetNoteService(int itineraryID);
        Task<BaseResponse> CreateRatingService(UserEventRating userRating);
        Task<BaseResponse> ModifyRatingService(UserEventRating userRating);
        Task<BaseResponse> CreateNoteService(ItineraryNote itineraryNote);
        Task<BaseResponse> ModifyNoteService(ItineraryNote itineraryNote);
    }
}
