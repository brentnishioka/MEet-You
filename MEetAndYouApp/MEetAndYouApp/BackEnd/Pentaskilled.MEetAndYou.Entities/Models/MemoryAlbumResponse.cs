using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pentaskilled.MEetAndYou.Entities.DBModels;


namespace Pentaskilled.MEetAndYou.Entities.Models
{
    public class MemoryAlbumResponse : BaseResponse
    {
        private List<Image> _data;

        public List<Image> Data { get; set; }

        public MemoryAlbumResponse(string v, bool v1) : base()
        {
            Data = new List<Image>();
        }

        public MemoryAlbumResponse(string message, bool isSuccessful, List<Image> data)
        {
            Message = message;
            IsSuccessful = isSuccessful;
            Data = data;
        }
    }
}
