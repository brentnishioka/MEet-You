using Microsoft.EntityFrameworkCore;
using Pentaskilled.MEetAndYou.DataAccess;
using Pentaskilled.MEetAndYou.Entities.DBModels;
using Pentaskilled.MEetAndYou.Entities.Models;
using Pentaskilled.MEetAndYou.Managers;
using Pentaskilled.MEetAndYou.Managers.Contracts;
using Xunit;
using Xunit.Abstractions;

namespace Pentaskilled.MEetAndYou.XUnitTests
{
    public class AccountDeletionTests
    {
        private readonly ITestOutputHelper _output;
        private MEetAndYouDBContext _dbContext;
        public static DbContextOptions<MEetAndYouDBContext> dbContextOptions { get; }
        public static string connectionString = "Filler connection string";

        static AccountDeletionTests()
        {
            dbContextOptions = new DbContextOptionsBuilder<MEetAndYouDBContext>()
                .UseSqlServer(connectionString)
                .Options;

        }
        public AccountDeletionTests(ITestOutputHelper output)
        {
            this._output = output;
            _dbContext = new MEetAndYouDBContext(dbContextOptions);
        }

        [Theory]
        [InlineData(9)]
        public async void AccountDeletionManTheory(int userID)
        {
            // Arrange
            IUMDAO umDAO = new UMDAO(_dbContext);
            IAccountDeletionManager accMan = new AccountDeletionManager(umDAO);

            // Act
            BaseResponse actual = await accMan.DeleteUser(userID);

            // Assert
            Assert.True(actual != null);
        }
    }
}
