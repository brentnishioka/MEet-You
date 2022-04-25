using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pentaskilled.MEetAndYou.Entities.DBModels;
using Pentaskilled.MEetAndYou.DataAccess.Contracts;

namespace Pentaskilled.MEetAndYou.DataAccess.Implementation
{
    public class MemoryAlbumDAO : IMemoryAlbumDAO
    {

        private string _connectionString;
        private MEetAndYouDBContext _dbContext;

        // Constructor
        public MemoryAlbumDAO()
        {
            _dbContext = new MEetAndYouDBContext();
        }

        static private string GetConnectionString()
        {
            return new ConnectionString().ToString();
        }



    }
}
