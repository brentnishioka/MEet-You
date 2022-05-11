using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pentaskilled.MEetAndYou.Entities.DBModels;
using Pentaskilled.MEetAndYou.Entities.Models;

namespace Pentaskilled.MEetAndYou.DataAccess.Contracts
{
    public interface IItineraryDAO
    {
        List<Itinerary> GetUserItineraries(int userID);

        Task<BaseResponse> ChangeItineraryName(int itineraryID,string name);

        Task<BaseResponse> ChangeItineraryRating(int itineraryID,int rating);





    }
}
