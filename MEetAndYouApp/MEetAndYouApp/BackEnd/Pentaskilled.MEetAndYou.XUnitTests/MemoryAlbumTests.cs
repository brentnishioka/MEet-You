using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Pentaskilled.MEetAndYou.DataAccess.Contracts;
using Pentaskilled.MEetAndYou.DataAccess.Implementation;
using Pentaskilled.MEetAndYou.Entities.DBModels;
using Pentaskilled.MEetAndYou.Entities.Models;
using Pentaskilled.MEetAndYou.Managers.Implementation;
using Pentaskilled.MEetAndYou.Services.Contracts;
using Pentaskilled.MEetAndYou.Services.Implementation;
using Xunit;
using Xunit.Abstractions;

namespace Pentaskilled.MEetAndYou.XUnitTests
{
    public class MemoryAlbumTests
    {
        private readonly ITestOutputHelper _output;
        private MEetAndYouDBContext _dbContext;
        public static DbContextOptions<MEetAndYouDBContext> dbContextOptions { get; }
        public static string connectionString = "Data Source=LAPTOP-5VDMOIMK;Initial Catalog=MEetAndYou-DB;Integrated Security=True";


        static MemoryAlbumTests()
        {
            dbContextOptions = new DbContextOptionsBuilder<MEetAndYouDBContext>()
                .UseSqlServer(connectionString)
                .Options;

        }


        public MemoryAlbumTests(ITestOutputHelper output)
        {
            this._output = output;
            _dbContext = new MEetAndYouDBContext(dbContextOptions);
        }

        [Theory]
        [InlineData("Alfred")]

        public async void GetExistingImage(string name)
        {
            MemoryAlbumDAO albumDAO = new MemoryAlbumDAO(_dbContext);
            Image imageRecord = new Image();


            _output.WriteLine("Pulling image form image record");
            MemoryAlbumResponse memoryAlbumResponse = await albumDAO.GetImageRecordAsync(name);
            _output.WriteLine(memoryAlbumResponse.Message);

            Assert.True(memoryAlbumResponse.IsSuccessful);
        }

    }
}
