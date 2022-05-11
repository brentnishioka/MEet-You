using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pentaskilled.MEetAndYou.Managers.Contracts;
using Pentaskilled.MEetAndYou.Services.Contracts;
using Pentaskilled.MEetAndYou.Services.Implementation;

namespace Pentaskilled.MEetAndYou.Managers.Implementation
{
    public class UADManager : IUADManager
    {
        private readonly IUserService _userService;

        public UADManager()
        {
            _userService = new UserService();
        }
        public Task<List<int>> GetItineraryCount()
        {
            throw new NotImplementedException();
        }

        public async Task<List<int>> GetRegisteredAccountNumber()
        {
            List<int> registrationCount = new List<int>();
            var presentDate = DateTime.Now.Date;

            var dateThreeMonthsAgo = presentDate.AddMonths(-3);

           // DateTime previous = new DateTime(presentDate.Year, dateThreeMonthsAgo, presentDate.Day);




            for (DateTime i = dateThreeMonthsAgo; i <= presentDate; i = i.AddDays(1))
            {
                registrationCount.Add(await _userService.GetRegistrationCountAsync(i.ToString()));
            }

            return registrationCount;
   
        }
    }
}
