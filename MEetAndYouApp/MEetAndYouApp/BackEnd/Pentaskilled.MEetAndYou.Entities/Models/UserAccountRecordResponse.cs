using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pentaskilled.MEetAndYou.Entities.DBModels;

namespace Pentaskilled.MEetAndYou.Entities.Models
{
    public class UserAccountRecordResponse : BaseResponse
    {
        private UserAccountRecord _data;

        public UserAccountRecord Data { get; set; }

        public UserAccountRecordResponse() : base()
        {
            Data = new UserAccountRecord();
        }

        public UserAccountRecordResponse(string message, bool isSuccessful, UserAccountRecord data)
        {
            Message = message;
            IsSuccessful = isSuccessful;
            Data = data;
        }
    }
}
