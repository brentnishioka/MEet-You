using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pentaskilled.MEetAndYou.DataAccess.Contracts;

namespace Pentaskilled.MEetAndYou.DataAccess
{
    public class AuthorizationDAO : IAuthorizationDAO
    {
        private string _connectionString;

        static private string GetConnectionString()
        {
            return new ConnectionString().ToString();
        }

        // Fix this please, the commands does not know the correct table and columns
        Task<int> IAuthorizationDAO.VerifyToken(string token)
        {
            _connectionString = GetConnectionString();
            SqlDataReader reader;
            int userIDCol = 1;      // User ID column to be read from    
            var userID = -1;

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                using (SqlCommand command = new SqlCommand("Select * from AccountRecords WHERE @token == HashToken", connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add("@token", SqlDbType.VarChar).Value = token;

                    connection.Open();
                    reader = command.ExecuteReader();
                    connection.Close();
                }
                userID = reader.GetFieldValue<int>(userIDCol);

            }
            catch (Exception ex)
            {
                return Task.FromResult(-1);
            }

            return Task.FromResult(userID);
        }

        Task<List<string>> GetRoles(string token)
        {
            //throw new NotImplementedException();
            _connectionString = GetConnectionString();
            SqlDataReader reader;
            int userIDCol = 1;      // User ID column to be read from    
            List<string> roles = new List<string>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                using (SqlCommand command = new SqlCommand("Select * from UserRole WHERE @userID == userID", connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add("@userID", SqlDbType.VarChar).Value = token;

                    connection.Open();
                    reader = command.ExecuteReader();
                    connection.Close();
                }
                while (reader.Read())
                {
                    roles.Append(reader.GetString(userIDCol));
                }
            }
            // Change this so that it signify a failure
            catch (Exception ex)
            {
                return Task.FromResult(new List<string>());
            }

            return Task.FromResult(roles);
        }
    }
}
