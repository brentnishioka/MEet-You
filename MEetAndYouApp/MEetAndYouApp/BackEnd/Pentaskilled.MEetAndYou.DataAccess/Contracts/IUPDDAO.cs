using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pentaskilled.MEetAndYou.Entities.DBModels;
using Pentaskilled.MEetAndYou.Entities.Models;

namespace Pentaskilled.MEetAndYou.DataAccess.Contracts
{
    public interface IUPDDAO
    {
        Task<ItineraryResponse> GetItineraryAsync(int userID);

        Task<RatingResponse> GetRatingsAsync(int itineraryID);

        Task<NoteResponse> GetNoteAsync(int itineraryID);

        Task<UserAccountRecordResponse> getUserAccount(int userID);

        // note make the fav itin response
    }
}
