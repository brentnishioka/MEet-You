using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pentaskilled.MEetAndYou.Entities.DBModels;
using Pentaskilled.MEetAndYou.Entities.Models;

namespace Pentaskilled.MEetAndYou.DataAccess
{
    public class CalendarDAO : ICalendarDAO
    {
        private readonly MEetAndYouDBContext _dbContext;

        // Constructor
        public CalendarDAO()
        {
            _dbContext = new MEetAndYouDBContext();
        }

        public CalendarDAO(MEetAndYouDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ItineraryResponse> GetUserItineraries(int userID, DateTime date)
        {
            List<Itinerary> distinctList = null;
            string message = "Get User in DAO itineraries is successful.";
            bool isSuccessful = true;
            try
            {
                List<Itinerary>  itineraries = await
                    (from itin in _dbContext.Itineraries.Include("Events")
                     from e in itin.Events
                     where itin.ItineraryOwner == userID &&
                     ((DateTime)e.EventDate).Year.Equals(date.Year) &&
                     ((DateTime)e.EventDate).Month.Equals(date.Month) &&
                     ((DateTime)e.EventDate).Day.Equals(date.Day)
                     select itin).ToListAsync<Itinerary>();

                distinctList = itineraries.Distinct().ToList();
                if(distinctList == null)
                {
                    return new ItineraryResponse("No itinerary found for user" + userID, isSuccessful, distinctList);
                }
            }
            catch (SqlException ex)
            {
                return new ItineraryResponse
                    ("Sql exception occur when getting itinerary \n" + ex.Message, false, distinctList);
            }
            catch (Exception ex)
            {
                return new ItineraryResponse
                                    ("Exception occur when trying to get itinerary by ID \n" + ex.Message, false, distinctList);
            }
            return new ItineraryResponse(message, isSuccessful, distinctList);
        }
        public DateTime DateConversion(string date)
        {
            CultureInfo ci = new CultureInfo("en-US");
            return DateTime.Parse(date, ci);
        }
    }

}
