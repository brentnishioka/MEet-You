using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pentaskilled.MEetAndYou.Entities.Models;

namespace Pentaskilled.MEetAndYou.Services.Contracts
{
    public interface IUPDService
    {
        Task<ItineraryResponse> GetItinerary(int userID);

        Task<RatingResponse> GetUserRatings(int itineraryID);

        Task<NoteResponse> GetNote(int itineraryID);

        Task<UserAccountRecordResponse> GetUser(int userID);



    }
}
