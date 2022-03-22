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
        /// <summary>
        /// Enter description for method anotherMethod.
        /// </summary>
        /// <param name="array1">Describe parameter.</param>
        /// <param name="array">Describe parameter.</param>
        /// <returns>Describe return value.</returns>
        public bool VerifyToken(int userID, string token)
        {
            _connectionString = GetConnectionString();
            bool result = false;      // User ID column to be read from    
            int rowsAffected;

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                // Call a procedure in the DB to compare the unhashed token with a hashed token. 
                using (SqlCommand command = new SqlCommand("EXEC [MEetAndYou].[VerifyUserToken](@userID, @token)", connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add("@token", SqlDbType.VarChar).Value = token;
                    command.Parameters.Add("@userID", SqlDbType.Int).Value = userID;

                    connection.Open();
                    //reader = command.ExecuteReader();
                    rowsAffected = (int) command.ExecuteScalar();
                    connection.Close();
                }
                //userID = reader.GetFieldValue<int>(userIDCol);

                if (rowsAffected > 0)
                {
                    result = true;
                }

            }
            catch (SqlException ex)
            {
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }

            return result;
        }

        // Async versin of the VerifyToken
        public async Task<bool> VerifyTokenAsync(int userID, string token)
        {
            _connectionString = GetConnectionString();
            bool result = false;      // User ID column to be read from    
            int rowsAffected;

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                // Call a procedure in the DB to compare the unhashed token with a hashed token. 
                using (SqlCommand command = new SqlCommand("EXEC [MEetAndYou].[VerifyUserToken](@userID, @token)", connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add("@token", SqlDbType.VarChar).Value = token;
                    command.Parameters.Add("@userID", SqlDbType.Int).Value = userID;

                    connection.Open();
                    //reader = command.ExecuteReader();
                    var rowAsyncTask = await command.ExecuteScalarAsync();
                    rowsAffected = (int) rowAsyncTask;
                    connection.Close();
                }
                //userID = reader.GetFieldValue<int>(userIDCol);

                if (rowsAffected > 0)
                {
                    result = true;
                }

            }
            catch (SqlException ex)
            {
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }

            return result;
        }

        // <params> token is not hashed, the DB will hashed this string to compare with the one in the database. 
        public Task<List<string>> GetRoles(string token)
        {
            //throw new NotImplementedException();
            _connectionString = GetConnectionString();
            SqlDataReader reader;
            int userIDCol = 1;      // User ID column to be read from    
            List<string> roles = new List<string>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                using (SqlCommand command = new SqlCommand("Select * from [MEetAndYou].[UserRole] WHERE @userID == userID", connection))
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
