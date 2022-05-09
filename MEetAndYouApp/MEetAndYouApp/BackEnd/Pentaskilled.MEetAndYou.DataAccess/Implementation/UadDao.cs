using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Pentaskilled.MEetAndYou.DataAccess.Contracts;
using Pentaskilled.MEetAndYou.Entities;

namespace Pentaskilled.MEetAndYou.DataAccess.Implementation
{
    public class UADDAO : IUADDAO
    {
        private string _connectionString;

        static private string GetConnectionString()
        {
            return new ConnectionString().ToString();
        }

        public List<SessionViews> getAvgViewTime()
        {
            throw new NotImplementedException();
        }

        public Dictionary<DateTime, int> getDailyLogin()
        {
            throw new NotImplementedException();
        }

        public Dictionary<DateTime, int> getDailyRegistration()
        {
            throw new NotImplementedException();
        }

        public List<SessionViews> getMostVisitedView()
        {
            throw new NotImplementedException();
        }


    }
}
