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
        Task<BaseResponse> LogCreatedRatingAsync(UserEventRating userRating);
        Task<BaseResponse> LogModifiedRatingAsync(UserEventRating userRating);
        Task<BaseResponse> LogCreatedNoteAsync(ItineraryNote itineraryNote);
        Task<BaseResponse> LogModifiedNoteAsync(ItineraryNote itineraryNote);
    }
}
