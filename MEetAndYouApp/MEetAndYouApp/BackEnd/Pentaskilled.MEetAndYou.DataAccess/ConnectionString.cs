namespace Pentaskilled.MEetAndYou.DataAccess
{
    public class ConnectionString
    {
        private string _connectionString;


        public ConnectionString()
        {
            _connectionString = @"Data Source=localhost;Initial Catalog=MEetAndYou-DB;User Id=MEetAndYouDBUser;Password=ray";
        }


        public override string ToString()
        {
            return _connectionString;
        }


    }
}
