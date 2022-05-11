using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pentaskilled.MEetAndYou.Entities.Models
{
    public class AccountCreationResponse
    {
        private string _message;
        private bool _isSuccessful;

        public string Message { get; set; }
        public bool IsSuccessful { get; set; }
        public AccountCreationResponse()
        {
            Message = "Default Error Message";
            IsSuccessful = false;
        }

        public AccountCreationResponse(string message, bool isSuccessful)
        {
            Message = message;
            IsSuccessful = isSuccessful;
        }
    }
}
