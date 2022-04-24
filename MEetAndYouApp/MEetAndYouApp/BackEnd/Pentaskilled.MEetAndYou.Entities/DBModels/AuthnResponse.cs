using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pentaskilled.MEetAndYou.Entities.DBModels
{
    public class AuthnResponse
    {
        private int _userID;
        private string _token;
        private List<string> _roles = new List<string>();

        public int UserID { get; set; }
        public string Token { get; set; }
        public List<string> Roles { get; set; }

        public AuthnResponse (int userID, string token, List<string> roles)
        {
            UserID = userID;
            Token = token;
            Roles = roles;
        }

        public AuthnResponse()
        {
            _userID = -1;
            _token = null;
            _roles = null;
        }
    }
}
