﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pentaskilled.MEetAndYou.Services.Contracts
{
    public interface IUserService
    {
        Task<int> GetRegistrationCountAsync(DateTime date);
    }
}
