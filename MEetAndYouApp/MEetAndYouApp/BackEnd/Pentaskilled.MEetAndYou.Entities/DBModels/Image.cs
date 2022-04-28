using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Pentaskilled.MEetAndYou.Entities.DBModels
{
    public partial class Image
    {
        [Key]
        public int ImageId { get; set; }
        public string ImageName { get; set; }
        public string ImageExtension { get; set; }
        public string ImagePath { get; set; }
        public int ItineraryId { get; set; }

        public IFormFile ImageFile { get; set; }
        public virtual Itinerary Itinerary { get; set; }
    }
}
