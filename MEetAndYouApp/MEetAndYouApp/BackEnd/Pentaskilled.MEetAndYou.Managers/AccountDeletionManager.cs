using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pentaskilled.MEetAndYou.DataAccess;
using Pentaskilled.MEetAndYou.Entities;
using 

namespace Pentaskilled.MEetAndYou.Managers
{
    public class AccountDeletionManager
    {
        private readonly IUMDAO _umDAO;
        private UserAccountEntity uAcc;
        
        public AccountDeletionManager()
        {
            _umDAO = new UMDAO();
            uAcc = new UserAccountEntity();
        }

        public bool DeleteUser(string json)    // Pass in the parameters from the API controller (id, token?)
        {
            try
            {
                var userEmail = json[0];
                var userToken = json[1];
                uAcc.Email = Convert.ToString(userEmail);
                _umDAO.DeleteAcc(uAcc);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
    }
}
