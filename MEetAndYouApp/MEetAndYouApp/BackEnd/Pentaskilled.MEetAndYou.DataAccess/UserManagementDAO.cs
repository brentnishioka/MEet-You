using Microsoft.Data.SqlClient;

namespace Pentaskilled.MEetAndYou.DataAccess
{
    public class UserManagementDAO
    {
        private readonly string _connectionString;

        public UserManagementDAO(string connectionString)
        {
            _connectionString = connectionString;
        }


        public bool VerifyUserInDB(string email)
        {


            return true;
        }
    }
}
