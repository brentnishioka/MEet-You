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

        /// <summary>
        /// Method to change the Itinerary name in the database 
        /// </summary>
        /// <param name="itineraryID"> The category of the log. </param>
        /// <param name="name"> The level of the log. </param
        /// <returns> Returns true if the logging process executed successfully false if otherwise. </returns>
        public Task<BaseResponse> ChangeItineraryName(int itineraryID, string name)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Method to change the user itinerary rating in the database.
        /// </summary>
        /// <param name="itineraryID"> ID of itinerary for db query </param>
        /// <param name="rating"> new rating for itinerary </param>
        /// <returns> Base response object that will be forwarded to the front end using api </returns>
        public Task<BaseResponse> ChangeItineraryRating(int itineraryID, int rating)
        {
            throw new NotImplementedException();
        }



        /// <summary>
        /// Method to get user itineraries from the database.
        /// </summary>
        /// <param name="userID"> The category of the log. </param>
        /// <returns> List of Itineraries pertaining to the user </returns>
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
