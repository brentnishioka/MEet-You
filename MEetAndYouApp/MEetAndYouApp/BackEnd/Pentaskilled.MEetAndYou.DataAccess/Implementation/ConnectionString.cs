namespace Pentaskilled.MEetAndYou.DataAccess
{
    public class ConnectionString
    {
        private string _connectionString;


        public ConnectionString()
        {
            _connectionString = @"Data Source=LAPTOP-5VDMOIMK\MSSQLSERVER01;Initial Catalog=MEetAndYou-DB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True";
        }
        //SQLEXPRESS

        public override string ToString()
        {
            return _connectionString;
        }


    }
}
