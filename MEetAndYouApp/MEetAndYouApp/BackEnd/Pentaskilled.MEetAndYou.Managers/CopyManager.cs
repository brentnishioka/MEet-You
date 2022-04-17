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

            //Get the list of events from the old itinerary
            //var eventList = _copyItineraryDAO.get
            var eventList =
                from itin in _dbContext.Itineraries
                from e in _dbContext.Events
                where itin.ItineraryId.Equals(e.EventId)
                select e;
                


               //from author in _dbcontext.Authors                 //blank used to be here
               //from book in dbcontext.Titles                    //blank used to be here
               //orderby author.LastName, author.FirstName
               //select new { author.FirstName, author.LastName, book.ISBN };
        }

    }
}
