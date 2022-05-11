using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pentaskilled.MEetAndYou.DataAccess.Contracts;
using Pentaskilled.MEetAndYou.DataAccess.Implementation;
using Pentaskilled.MEetAndYou.Services.Contracts;

namespace Pentaskilled.MEetAndYou.Services.Implementation
{
    public class UserService: IUserService
    {

        private readonly UserDAO _userDAO;
        public UserService()
        {
            _userDAO = new UserDAO();
        }
        public async Task<int> GetRegistrationCountAsync(string date)
        {
            return await _userDAO.GetRegisteredCount(date);
        }



       
    }
}
