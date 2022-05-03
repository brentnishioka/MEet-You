using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pentaskilled.MEetAndYou.Entities.Models;
using Pentaskilled.MEetAndYou.Entities.DBModels;

namespace Pentaskilled.MEetAndYou.DataAccess.Contracts
{
    public interface IMemoryAlbumDAO
    {
        Task<MemoryAlbumResponse> GetImageRecordAsync(string imageName);
        
        // Given an itineraryID, adds a imageRecord to the itinerary by using navigation from EF
        // Adds the UserAccountID and Itinerary to junction table UserItinerary
        Task<MemoryAlbumResponse> AddImageToItineraryAsync(Image imageRecord, int itineraryID);

        // Given an itineraryID, removes a imageRecord from the itinerary by using navigation from EF
        // Removes the UserAccountID and Itinerary from junction table UserItinerary
        Task<MemoryAlbumResponse> RemoveImageFromItineraryAsync(Image imageRecord, int itineraryID);
    }
}
