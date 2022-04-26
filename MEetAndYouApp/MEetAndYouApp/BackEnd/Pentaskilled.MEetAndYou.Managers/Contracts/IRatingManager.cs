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
        BaseResponse CreateRating(int eventID, int itineraryID, int userRating);
        BaseResponse ModifyRating(int eventID, int itineraryID, int userRating);
        BaseResponse CreateItineraryNote(int itineraryID, string noteContent);
        BaseResponse ModifyItineraryNote(int itineraryID, string noteContent);
    }
}
