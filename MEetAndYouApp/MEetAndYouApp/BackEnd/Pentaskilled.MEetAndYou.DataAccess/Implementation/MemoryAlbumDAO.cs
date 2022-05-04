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

        /// <summary>
        /// Gets an Image object from the database in the Images table
        /// </summary>
        /// <param name="itineraryID"> Itinerary value to be locate </param>
        /// <returns>  
        ///     A MemoryAlbumResponse object that has the status of the operation and message. 
        /// </returns>
        public async Task<MemoryAlbumResponse> GetImageRecordAsync(int itineraryID)
        {
            List<Image> distinctList = null;
            string message = "Get Images in DAO images is successful";
            bool isSuccessful = true;


            // Execute a LINQ-to-Entity query
            try
            {
                List<Image> images = await (
                 from image in _dbContext.Images
                 where image.ItineraryId == itineraryID
                 select image).ToListAsync<Image>();


                distinctList = images.Distinct().ToList();

                if (distinctList == null)
                {
                    return new MemoryAlbumResponse("No images found for the itineraryID" + itineraryID, isSuccessful, null);
                }

                return new MemoryAlbumResponse(message, isSuccessful, distinctList);

            }
            catch (SqlException ex)
            {
                return new MemoryAlbumResponse
                    ("Sql exception occur when getting itinerary \n" + ex.Message, false, distinctList);
            }
            catch (Exception ex)
            {
                return new MemoryAlbumResponse
                ("Exception occur when trying to get itinerary by ID \n" + ex.Message, false, distinctList);
            }
        }

        /// <summary>
        /// Adds an Image object from the database in the Images table
        /// </summary>
        /// <param name="itineraryID"> Itinerary value to be locate </param>
        /// <returns>  
        ///     A MemoryAlbumResponse object that has the status of the operation and message. 
        /// </returns>
        public async Task<MemoryAlbumResponse> AddImageToItineraryAsync(string ImageName, string ImageExtension, string ImagePath, int itineraryID)
        {
            Itinerary itin;

            Image imageModel;
            try
            {
                imageModel = new Image(ImageName, ImageExtension, ImagePath, itineraryID);

                var uniqueImages = await
                   (from images in _dbContext.Images
                    where images.ItineraryId == itineraryID
                    group images by new { images.ItineraryId, images.ImageId } into grp
                    select new {
                        grp.Key.ItineraryId,
                        grp.Key.ImageId,
                    }).CountAsync();

                var uniqueNames = await
               (from images in _dbContext.Images
                where images.ImageName == ImageName
                group images by new { images.ImageName } into grp
                select new {
                    grp.Key.ImageName,

                }).CountAsync();

                // Add user if existing users in itinerary is less than 5
                if (uniqueImages < 10 && uniqueNames < 0)
                {
                    // Add object to context
                    _dbContext.Entry(imageModel).State = EntityState.Added;

                    // Save changes to context
                    var image = await _dbContext.SaveChangesAsync();
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

            return new MemoryAlbumResponse("Image successfully added", true, _dbContext.Images.ToList());
        }

        /// <summary>
        /// Removes an Image object from the database in the Images table
        /// </summary>
        /// <param name="itineraryID"> Itinerary value to be locate </param>
        /// <returns>  
        ///     A MemoryAlbumResponse object that has the status of the operation and message. 
        /// </returns>
        public async Task<MemoryAlbumResponse> RemoveImageFromItineraryAsync(string imageName, int itineraryID)
        {
            Itinerary itin;
            Image imageModel;
            string message = "Delete Images in DAO images is successful";
            bool isSuccessful = true;
            try
            {

                List<Image> distinctList = null;
                
                //Acquires list of Image objects that match parameter conditions
                List<Image> images = await (
                 from image in _dbContext.Images
                 where image.ItineraryId == itineraryID && image.ImageName == imageName
                 select image).ToListAsync<Image>();
                 distinctList = images.Distinct().ToList();

                foreach(Image image in distinctList)
                {
                    _dbContext.Entry(image).State = EntityState.Deleted;
                }

                await _dbContext.SaveChangesAsync();
               

                return new MemoryAlbumResponse(message, isSuccessful, distinctList);
            }
            catch (DbUpdateException)
            {
                return new MemoryAlbumResponse("Database failed to remove image", false, null);
            }
            catch (NullReferenceException)
            {
                return new MemoryAlbumResponse("Database could not find image", false, null);
            }
            return new MemoryAlbumResponse("Image is successfully removed", true, itin.Images.ToList());

        }
    }
}
