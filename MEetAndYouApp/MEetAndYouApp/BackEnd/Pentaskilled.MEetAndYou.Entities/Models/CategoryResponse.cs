using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pentaskilled.MEetAndYou.Entities.DBModels;

namespace Pentaskilled.MEetAndYou.Entities.Models
{
    public class CategoryResponse : BaseResponse
    {
        private List<Category> _data;

        public List<Category> Data { get; set; }

        public CategoryResponse() : base()
        {
            Data = new List<Category>();
        }

        public CategoryResponse(string message, bool isSuccessful, List<Category> data)
        {
            Message = message;
            IsSuccessful = isSuccessful;
            Data = data;
        }
    }
}
