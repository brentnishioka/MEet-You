using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pentaskilled.MEetAndYou.DataAccess.Contracts;
using Pentaskilled.MEetAndYou.Entities.DBModels;

namespace Pentaskilled.MEetAndYou.DataAccess.Implementation
{
    public class UserDAO : IUserDAO

    {
        private MEetAndYouDBContext _dbContext;
        
        public UserDAO()
        {
            this._dbContext = new MEetAndYouDBContext();
        }

        public UserDAO(MEetAndYouDBContext _dbContext)
        {
            this._dbContext = _dbContext;
        }

        /// <summary>
        /// Method to get user account info from the database
        /// </summary>
        /// <param name="userID"> id of the user</param>
        /// <returns> User account record </returns>
        public async Task<UserAccountRecord> getUserAccount(int userID)
        {
            return await _dbContext.UserAccountRecords.FindAsync(userID);
        }
    }
}
