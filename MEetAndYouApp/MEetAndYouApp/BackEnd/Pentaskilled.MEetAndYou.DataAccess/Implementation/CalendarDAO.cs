using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pentaskilled.MEetAndYou.Entities.DBModels;

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

        public async Task<List<Itinerary>> GetUserItineraries(int userID, DateTime date)
        {
            //var dbcontext = new MEetAndYouDBContext();
            List<Itinerary> itineraries;
            List<Itinerary> distinctList;
            try
            {
                //itineraries = await
                //(from itin in _dbContext.Itineraries.Include("Events")
                // where itin.ItineraryOwner == userID
                // select itin).ToListAsync<Itinerary>();

                //Get itineraries where all events within the itineraries are on the same date
                //itineraries = await
                //    (from itin in _dbContext.Itineraries.Include("Events")
                //     from e in itin.Events
                //     where itin.ItineraryOwner == userID && (DateTime) e.EventDate == date
                //     select itin).ToListAsync<Itinerary>();

                itineraries = await
                    (from itin in _dbContext.Itineraries.Include("Events")
                     from e in itin.Events
                     where itin.ItineraryOwner == userID &&
                     ((DateTime)e.EventDate).Year.Equals(date.Year) &&
                     ((DateTime)e.EventDate).Month.Equals(date.Month) &&
                     ((DateTime)e.EventDate).Day.Equals(date.Day)
                     select itin).ToListAsync<Itinerary>();

                distinctList = itineraries.Distinct().ToList();

                //itineraries =
                //        (List<Itinerary>)(from itin in _dbContext.Itineraries.Include("Events")
                //        from e in itin.Events
                //        where itin.ItineraryOwner == userID &&
                //        ((DateTime)e.EventDate).Year.Equals(date.Year) &&
                //        ((DateTime)e.EventDate).Month.Equals(date.Month) &&
                //        ((DateTime)e.EventDate).Day.Equals(date.Day)
                //        group itin.ItineraryId by e into g
                //        select new { itinList = g.ToList() });

            }
            catch (SqlException ex)
            {
                Console.WriteLine("Sql exception occur when getting itinerary");
                Console.WriteLine(ex.Message);
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception occur when trying to get itinerary by ID");
                Console.WriteLine(ex.Message);
                return null;
            }
            return distinctList;
        }
        public DateTime DateConversion(string date)
        {
            CultureInfo ci = new CultureInfo("en-US");
            return DateTime.Parse(date, ci);
        }
    }

}
