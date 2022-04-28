using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pentaskilled.MEetAndYou.Entities.DBModels;

namespace Pentaskilled.MEetAndYou.DataAccess
{
    public class CalendarDAO : ICalendarDAO
    {
        private string _connectionString;
        private MEetAndYouDBContext _dbContext;

        // Constructor
        public CalendarDAO()
        {
            _dbContext = new MEetAndYouDBContext();
        }

        static private string GetConnectionString()
        {
            return new ConnectionString().ToString();
        }

        public async Task<List<Itinerary>> GetUserItineraries(int userID)
        {
            //var dbcontext = new MEetAndYouDBContext();
            List<Itinerary> itineraries;
            try
            {
                itineraries = await
                (from itin in _dbContext.Itineraries.Include("Events")
                 where itin.ItineraryOwner == userID
                 select itin).ToListAsync<Itinerary>();
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
            return itineraries;
        }
    }
}
