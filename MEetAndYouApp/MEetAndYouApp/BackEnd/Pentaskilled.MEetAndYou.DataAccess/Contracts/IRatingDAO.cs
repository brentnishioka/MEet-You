using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pentaskilled.MEetAndYou.Entities.Models;
using Pentaskilled.MEetAndYou.Entities.DBModels;

namespace Pentaskilled.MEetAndYou.DataAccess.Contracts
{
    public interface IRatingDAO
    {
        Task<BaseResponse> AddRatingInDBAsync(UserEventRating userRating);
        Task<BaseResponse> ModifyRatingInDBAsync(UserEventRating userRating);
        Task<BaseResponse> AddNoteInDBAsync(ItineraryNote itineraryNote);
        Task<BaseResponse> ModifyNoteInDBAsync(ItineraryNote itineraryNote);
    }
}
