namespace Pentaskilled.MEetAndYou.DataAccess
{
    public class ConnectionString
    {
        private string _connectionString;


        public ConnectionString()
        {
            _connectionString = @"Data Source=DESKTOP-0QA4EN0\SQLEXPRESS;Initial Catalog=MEetAndYou-DB;Integrated Security=True";
        }


        public override string ToString()
        {
            return _connectionString;
        }


    }
}
