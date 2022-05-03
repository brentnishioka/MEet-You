using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pentaskilled.MEetAndYou.Entities.DBModels;
using Pentaskilled.MEetAndYou.DataAccess.Contracts;
using Pentaskilled.MEetAndYou.Entities.Models;

namespace Pentaskilled.MEetAndYou.DataAccess.Implementation
{
    public class MemoryAlbumDAO : IMemoryAlbumDAO
    {

        private readonly MEetAndYouDBContext _dbContext;

        // Constructor
        public MemoryAlbumDAO()
        {
            _dbContext = new MEetAndYouDBContext();
        }

        public MemoryAlbumDAO(MEetAndYouDBContext dbContext)
        {
            _dbContext = new MEetAndYouDBContext();
        }

        public async Task<MemoryAlbumResponse> GetImageRecordAsync(string imageName)
        {
            Image imageModel;


            // Execute a LINQ-to-Entity query
             imageModel = await
                (from image in _dbContext.Images
                 where image.ImageName == imageName
                 select image).FirstAsync<Image>();
/*
            // If imageModel is null, set error message and isSuccessful to false 
            if (imageModel == null)
            {
                return new MemoryAlbumResponse("Unable to find image by name", false, imageModel.);
            }

            // Successfully pulled imageModel from context using user email
            else
            {
                return new MemoryAlbumResponse("Successfully found image by name", true, imageModel);
            }*/
            return null;
        }
        public async Task<MemoryAlbumResponse> AddImageToItineraryAsync(Image imageRecord, int itineraryID)
        {
            Itinerary itin;
            Image imageModel;
            try
            {
                itin = await _dbContext.Itineraries.Include(i => i.UserItineraries).FirstOrDefaultAsync(i => i.ItineraryId == itineraryID);
                imageModel = new Image(imageRecord.ImageName, imageRecord.ImageExtension, imageRecord.ImagePath, itineraryID);

                var uniqueImages = await
                   (from images in _dbContext.Images
                    where images.ItineraryId == itineraryID
                    group images by new { images.ItineraryId, images.ImageId } into grp
                    select new {
                        grp.Key.ItineraryId,
                        grp.Key.ImageId,
                    }).CountAsync();

                // Add user if existing users in itinerary is less than 5
                if (uniqueImages < 10)
                {
                    // Add object to context
                    itin.Images.Add(imageModel);
                    _dbContext.Entry(itin).State = EntityState.Modified;

                    // Save changes to context
                    await _dbContext.SaveChangesAsync();
                }
                else
                {
                    return new MemoryAlbumResponse("Max images reached, please remove an image", false, null);
                }
            }
            catch (InvalidOperationException)
            {
                return new MemoryAlbumResponse("User already added", false, null);
            }
            catch (DbUpdateException)
            {
                return new MemoryAlbumResponse("Database failed to add image", false, null);
            }
            catch (NullReferenceException)
            {
                return new MemoryAlbumResponse("Database could not find image", false, null);
            }

            return new MemoryAlbumResponse("Image successfully added", true, itin.Images.ToList());
        }

        public async Task<MemoryAlbumResponse> RemoveImageFromItineraryAsync(Image imageRecord, int itineraryID)
        {
            Itinerary itin;
            Image imageModel;
            try
            {
                // Find associated itinerary
                itin = await _dbContext.Itineraries.Include(i => i.UserItineraries).FirstOrDefaultAsync(i => i.ItineraryId == itineraryID);

                // Create Image object to be removed
                imageModel = new Image(imageRecord.ImageName, imageRecord.ImageExtension, imageRecord.ImagePath, itineraryID);

                // Find object from context
                var image = await _dbContext.Images.FirstOrDefaultAsync(i => i == imageModel);

            }
            catch (DbUpdateException)
            {
                return new MemoryAlbumResponse("Database failed to remove user", false, null);
            }
            catch (NullReferenceException)
            {
                return new MemoryAlbumResponse("Database could not find user", false, null);
            }
            return new MemoryAlbumResponse("Image is successfully removed", true, itin.Images.ToList());

        }
    }
}
