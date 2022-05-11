namespace Pentaskilled.MEetAndYou.DataAccess
{
    public class ConnectionString
    {
        private string _connectionString;


        public ConnectionString()
        {
            _connectionString = @"Data Source=meetandyou-db.cyakceoi9n4j.us-west-1.rds.amazonaws.com;Initial Catalog=MEetAndYou-DB;User Id=admin;Password=AlatreonFatalisVelkhana;Connect Timeout=30;TrustServerCertificate=True;";
        }
        //SQLEXPRESS

        public override string ToString()
        {
            return _connectionString;
        }


    }
}
