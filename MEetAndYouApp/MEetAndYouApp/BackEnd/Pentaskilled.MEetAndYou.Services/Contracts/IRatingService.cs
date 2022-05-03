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
        Task<BaseResponse> LogCreatedRatingAsync(UserEventRating userRating, int userID);
        Task<BaseResponse> LogModifiedRatingAsync(UserEventRating userRating, int userID);
        Task<BaseResponse> LogCreatedNoteAsync(ItineraryNote itineraryNote, int userID);
        Task<BaseResponse> LogModifiedNoteAsync(ItineraryNote itineraryNote, int userID);
    }
}
