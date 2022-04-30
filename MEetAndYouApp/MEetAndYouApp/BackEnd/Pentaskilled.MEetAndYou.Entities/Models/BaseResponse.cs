using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pentaskilled.MEetAndYou.Entities.Models
{
    public class BaseResponse
    {
        private string _message;
        private bool _isSuccessful;

        public string Message { get; set; }
        public bool IsSuccessful { get; set; }
        public BaseResponse()
        {
            Message = "Default Error Message";
            IsSuccessful = false;
        }

        public BaseResponse(string message, bool isSuccessful)
        {
            Message = message;
            IsSuccessful = isSuccessful;
        }
    }
}
