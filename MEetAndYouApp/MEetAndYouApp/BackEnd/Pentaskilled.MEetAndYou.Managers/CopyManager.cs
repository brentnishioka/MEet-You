using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pentaskilled.MEetAndYou.Entities.DBModels;

namespace Pentaskilled.MEetAndYou.Managers
{
    public class CopyManager
    {
        private readonly _dbContext;
        private readonly _copyItinerary;
        public Itinerary LoadItineraryInfo(string itineraryName)
        {
            _dbContext = dBContext;
            _copyItineraryDAO = new CopyItineraryDAO();
        }

        public Itinerary LoadItineraryInfo(int itineraryID)
        {
            //Get the itinerary with that ID
            Itinerary itinerary = _copyItineraryDAO.GetItinerary(itineraryID).Result;

            //var itin2 =
            //    (from itin in _dbContext.Itineraries
            //     from e in _dbContext.Events
            //     select new { itin.ItineraryId, itin.ItineraryName, itin.Rating, e.EventId, e.EventName }).ToList();

            Itinerary itinEager =
                (from itin in _dbContext.Itineraries.Include("Events")
                 where itin.ItineraryId == itineraryID
                 select itin).FirstOrDefault<Itinerary>();

            //Get the list of events from the old itinerary
            ICollection < Event > eventList = itinerary.Events;
            ICollection<Image> imageList = itinerary.Images;

            //Itinerary newItin = new Itinerary { ItineraryName= "Entity Itin",Rating=5 , ItineraryOwner=};
            Event randomEvent = new Event {
                EventName = "Testing Event", Description = "Testing to see if it works",
                Address = "library basement", Price = 5, EventDate = DateTime.Now
            };
            //randomEvent.EntityState = EntityState.Added;

            _dbContext.SaveChanges();

            return itinEager;
        }

        public bool AddEvent()
        {
            Event randomEvent = new Event {
                EventName = "Library study Group", Description = "grind",
                Address = "library basement", Price = 5, EventDate = DateTime.Now
            };
            //randomEvent.EntityState = EntityState.Added;
            Event deleteObj = _dbContext.Events.Find(16);
            _dbContext.Entry(deleteObj).State = EntityState.Deleted;
            //_dbContext.Set<Event>().Add(randomEvent);

            _dbContext.SaveChanges();

            return true;
        }

    }
}
