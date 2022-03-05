using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pentaskilled.MEetAndYou.DataAccess.Contracts
{
    public interface IAuthorizationDAO
    {
        Task<int> VerifyToken(string token);
    }
}
