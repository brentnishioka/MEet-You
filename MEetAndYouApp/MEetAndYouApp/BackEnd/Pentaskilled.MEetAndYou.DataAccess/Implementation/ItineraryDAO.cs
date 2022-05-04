using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pentaskilled.MEetAndYou.Entities.DBModels;
using Pentaskilled.MEetAndYou.Entities.Models;

namespace Pentaskilled.MEetAndYou.DataAccess.Implementation
{
public class ItineraryDAO : ICalendarDAO
{    
    private MEetAndYouDBContext _dbContext;


    public ItineraryDAO(MEetAndYouDBContext dbContext)
    {
            this._dbContext = dbContext;
    }

        public DateTime DateConversion(string date)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Itinerary>> GetUserItineraries(int userID)
    { 
        List<Itinerary> itineraries;
        try
        {
            itineraries =
            (from itin in _dbContext.Itineraries.Include("ItineraryOwnerNavigation")
                where itin.ItineraryOwner == userID
                select itin).ToList<Itinerary>();
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

        public Task<ItineraryResponse> GetUserItineraries(int userID, DateTime date)
        {
            throw new NotImplementedException();
        }
    }
}
