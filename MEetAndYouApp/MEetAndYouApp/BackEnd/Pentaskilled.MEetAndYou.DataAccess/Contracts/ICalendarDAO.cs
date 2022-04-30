using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pentaskilled.MEetAndYou.Entities.DBModels;
using Pentaskilled.MEetAndYou.Entities.Models;

namespace Pentaskilled.MEetAndYou.DataAccess
{
    public interface ICalendarDAO
    {
        Task<ItineraryResponse> GetUserItineraries(int userID, DateTime date);
        DateTime DateConversion(string date);
    }
}
