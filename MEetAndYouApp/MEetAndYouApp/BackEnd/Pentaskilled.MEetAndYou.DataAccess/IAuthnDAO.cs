using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pentaskilled.MEetAndYou.DataAccess
{
    public interface IAuthnDAO
    {
        Task<bool> ValidateCredentials(string email, string password);
    }
}
