namespace Pentaskilled.MEetAndYou.DataAccess
{
    public class ConnectionString
    {
        private string _connectionString;


        public ConnectionString()
        {
            _connectionString = @"Data Source=DESKTOP-MIH6TNI\MSSQLSERVER01;Initial Catalog=MEetAndYou-DB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False";
        }
        //SQLEXPRESS

        public override string ToString()
        {
            return _connectionString;
        }


    }
}
