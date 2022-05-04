﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pentaskilled.MEetAndYou.DataAccess.Implementation;
using Pentaskilled.MEetAndYou.Entities.DBModels;
using Pentaskilled.MEetAndYou.DataAccess;
using Pentaskilled.MEetAndYou.DataAccess.Implementation;
using Pentaskilled.MEetAndYou.Services.Implementation;
using Pentaskilled.MEetAndYou.Entities.DBModels;
using Pentaskilled.MEetAndYou.Entities.Models;
using Pentaskilled.MEetAndYou.Managers.Contracts;
using Pentaskilled.MEetAndYou.DataAccess.Contracts;

namespace Pentaskilled.MEetAndYou.Managers.Implementation
{
    public class MemoryAlbumManager : IMemoryAlbumManager
    {

        private readonly MEetAndYouDBContext _dbContext;
        private readonly IMemoryAlbumDAO _memoryAlbumDAO;
        //private readonly IMem

        public MemoryAlbumManager(IMemoryAlbumDAO memoryAlbumDAO, MEetAndYouDBContext dbContext)
        {
            _memoryAlbumDAO = memoryAlbumDAO;
            _dbContext = dbContext;
        }

        public BaseResponse AddImagesToItineraryAsync( string imageName, string imageExtension, string imagePath, int itineraryID)
        {
            Image imageRecord = new Image(imageName, imageExtension, imagePath, itineraryID);
            BaseResponse addImages =  _memoryAlbumDAO.AddImageToItineraryAsync(imageName,imageExtension, imagePath, itineraryID).Result;

            return addImages;
           

        }

        public async Task<MemoryAlbumResponse> RemoveImagesFromItineraryAsync(string imageName, string imageExtension, string imagePath, int itineraryID)
        {
            MemoryAlbumResponse memoryAlbumResponse;
            try
            {
                //Validate inputs
                bool isValidImageName = Validator.IsValidString(imageName);
                if (!isValidImageName) { return new MemoryAlbumResponse("Invalid image name", false, null); }

                bool isExtension = Validator.IsValidExtension(imageName);
                if (!isValidImageName) { return new MemoryAlbumResponse("Invalid image name", false, null); }



                memoryAlbumResponse = await _memoryAlbumDAO.GetImageRecordAsync(itineraryID);
                if (memoryAlbumResponse.IsSuccessful == false)
                {
                    return new MemoryAlbumResponse(memoryAlbumResponse.Message, false, null);
                }


                memoryAlbumResponse = await _memoryAlbumDAO.RemoveImageFromItineraryAsync(imageName, itineraryID);

            }
            catch (Exception ex)
            {
                return new MemoryAlbumResponse("Add image in Manager failed: \n" + ex.Message, false, null);
            }

            return memoryAlbumResponse;
        }


    }
}
