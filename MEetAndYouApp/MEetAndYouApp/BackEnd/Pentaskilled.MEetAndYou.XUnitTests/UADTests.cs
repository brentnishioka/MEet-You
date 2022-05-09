using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pentaskilled.MEetAndYou.API.Controllers;
using Pentaskilled.MEetAndYou.DataAccess.Contracts;
using Pentaskilled.MEetAndYou.DataAccess.Implementation;
using Pentaskilled.MEetAndYou.Entities.DBModels;
using Pentaskilled.MEetAndYou.Entities.Models;
using Pentaskilled.MEetAndYou.Managers.Contracts;
using Pentaskilled.MEetAndYou.Managers.Implementation;
using Pentaskilled.MEetAndYou.Services.Contracts;
using Pentaskilled.MEetAndYou.Services.Implementation;
using Xunit;
using Xunit.Abstractions;


namespace Pentaskilled.MEetAndYou.XUnitTests
{
    public class UADTests
    {

        private readonly ITestOutputHelper _output;
        private MEetAndYouDBContext _dbContext;
        public static DbContextOptions<MEetAndYouDBContext> dbContextOptions { get; }
        public static string connectionString = "Data Source=meetandyou-db.cyakceoi9n4j.us-west-1.rds.amazonaws.com;Initial Catalog=MEetAndYou-DB;User Id=admin;Password=TeostraLunastraAlatreon;Connect Timeout=30;TrustServerCertificate=True;";

        static UADTests()
        {
            dbContextOptions = new DbContextOptionsBuilder<MEetAndYouDBContext>()
                .UseSqlServer(connectionString)
                .Options;

        }

        public UADTests(ITestOutputHelper output)
        {
            _output = output;
            _dbContext = new MEetAndYouDBContext(dbContextOptions);
        }
/*
        [Theory]
        [InlineData()]
        public GetRegistrationCount()
        {

        }*/

    }
}
