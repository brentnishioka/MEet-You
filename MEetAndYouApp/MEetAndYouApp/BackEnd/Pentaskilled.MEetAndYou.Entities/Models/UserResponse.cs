using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pentaskilled.MEetAndYou.Entities.Models
{
    public class UserResponse : BaseResponse
    {

        private User _data;

        public User Data { get; set; }

        public UserResponse() : base()
        {
            Data = new User("temp val", "temp val");
        }

        public UserResponse(string message, bool isSuccessful, User data)
        {
            Message = message;
            IsSuccessful = isSuccessful;
            Data = data;
        }









    }
}
