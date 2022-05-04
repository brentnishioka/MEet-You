using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pentaskilled.MEetAndYou.Entities.DBModels;
using Pentaskilled.MEetAndYou.Entities.Models;
using Pentaskilled.MEetAndYou.DataAccess.Contracts;
namespace Pentaskilled.MEetAndYou.DataAccess.Implementation
{
    public class ItineraryDAO : IItineraryDAO
    {
        private MEetAndYouDBContext _dbContext;


        public ItineraryDAO()
        {
            _dbContext = new MEetAndYouDBContext();
        }

        public ItineraryDAO(MEetAndYouDBContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public Task<BaseResponse> ChangeItineraryName(string name)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse> ChangeItineraryRating(int itineraryID)
        {
            throw new NotImplementedException();
        }
        public List<Itinerary> GetUserItineraries(int userID)
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


    }
}
