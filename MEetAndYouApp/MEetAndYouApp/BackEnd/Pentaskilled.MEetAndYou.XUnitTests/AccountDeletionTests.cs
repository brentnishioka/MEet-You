using Pentaskilled.MEetAndYou.Managers;
using Xunit;

namespace Pentaskilled.MEetAndYou.XUnitTests
{
    public class AccountDeletionTests
    {
        [Theory]
        [InlineData("{_email: viviand2465@gmail.com, _token: token}")]
        public void AccountDeletionManTheory(string json)
        {
            AccountDeletionManager _accMan = new AccountDeletionManager();
            bool actual = _accMan.DeleteUser(json);

            Assert.True(actual);
        }
    }
}
