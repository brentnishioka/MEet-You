using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Data;

namespace Pentaskilled.MEetAndYou.DataAccess
{
    public class AuthnDAO : IAuthnDAO
    {
        // TODO: move this to our config file instead of it being directly in the code.
        private string _connectionString;

        // GetConnectionString() from https://docs.microsoft.com/en-us/dotnet/api/system.data.sqlclient.sqlconnection.connectionstring?view=dotnet-plat-ext-6.0
        static private string GetConnectionString()
        {
            return new ConnectionString().ToString();
        }

        public Task<bool> ValidateCredentials(string email, string password)
        {
            _connectionString = GetConnectionString();
            int rowsReturned = -1; 

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                using (SqlCommand command = new SqlCommand("SELECT [MEetAndYou].[ValidateCredentialsInDB](@email, @password)", connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add("@email", SqlDbType.VarChar).Value = email;
                    command.Parameters.Add("@password", SqlDbType.VarChar).Value = password;

                    connection.Open();
                    rowsReturned = (int) command.ExecuteScalar();
                    connection.Close();
                }

            }
            catch(Exception ex)
            {
                return Task.FromResult(false);  
            }

            return Task.FromResult(Convert.ToBoolean(rowsReturned)); 
        }

    }
}
