using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pentaskilled.MEetAndYou.DataAccess.Implementation;
using Pentaskilled.MEetAndYou.Entities.DBModels;

namespace Pentaskilled.MEetAndYou.Managers
{
    public class CopyManager
    {
        private readonly MEetAndYouDBContext _dbContext;
        private readonly CopyItineraryDAO _copyItineraryDAO;

        public CopyManager(MEetAndYouDBContext dBContext)
        {
            _dbContext = dBContext;
            _copyItineraryDAO = new CopyItineraryDAO();
        }

        public Itinerary LoadItineraryInfo(int itineraryID)
        {
            //Get the itinerary with that ID
            var itinerary = _copyItineraryDAO.GetItinerary(itineraryID).Result;
        }

    }
}
