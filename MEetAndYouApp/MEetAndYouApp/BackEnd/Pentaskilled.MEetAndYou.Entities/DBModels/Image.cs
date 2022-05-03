using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Pentaskilled.MEetAndYou.Entities.DBModels
{
    public partial class Image
    {
        public Image()
        {

        }

        public Image(string imageName, string imageExtension, string imagePath, int itineraryID)
        {

            ImageName = imageName;  
            ImageExtension = imageExtension;    
            ImagePath = imagePath;
            ItineraryId = itineraryID;  
        }

        public int ImageId { get; set; }
        public string ImageName { get; set; }
        public string ImageExtension { get; set; }
        public string ImagePath { get; set; }
        public int ItineraryId { get; set; }

        public IFormFile ImageFile { get; set; }
        public virtual Itinerary Itinerary { get; set; }

        public static implicit operator Image(List<Image> v)
        {
            throw new NotImplementedException();
        }
    }
}
