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

        // Todo: GetPhoneNumber function or store procedure does not exist in the database
        public Task<string> GetPhoneNum(string email, string password)
        {
            _connectionString = GetConnectionString();
            string phoneNumber;

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                using (SqlCommand command = new SqlCommand("SELECT [MEetAndYou].[GetPhoneNumber](@email, @password)", connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add("@email", SqlDbType.VarChar).Value = email;
                    command.Parameters.Add("@password", SqlDbType.VarChar).Value = password;

                    connection.Open();
                    phoneNumber = (string) command.ExecuteScalar();
                    connection.Close();
                }

            }
            catch (Exception ex)
            {
                return Task.FromException<string>(ex);
            }

            return Task.FromResult(phoneNumber);
        }

        /// <summary>
        /// Save a randomly generated token of length 25 to the "UserToken" table.
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="token"></param>
        /// <returns>  
        ///     True -> If token is saved successfully.
        ///     False -> if the token failed to be saved.
        /// </returns>
        public Task<bool> SaveToken(int userID, string token)
        {
            _connectionString = GetConnectionString();
            int numRows = -1;
            bool result = false;
            string dateCreated = DateTime.UtcNow.ToString();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                using (SqlCommand command = new SqlCommand("[MEetAndYou].[StoreUserToken]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@userID", SqlDbType.Int).Value = userID;
                    command.Parameters.Add("@token", SqlDbType.VarChar).Value = token;
                    command.Parameters.Add("@dateCreated", SqlDbType.VarChar).Value = dateCreated;

                    connection.Open();
                    numRows = command.ExecuteNonQuery();
                    connection.Close();
                }
                if (numRows > 0)
                {
                    result = true;
                }

            }
            catch(SqlException ex)
            {
                Console.WriteLine("SQL exception when saving token to the DB!!");
                return Task.FromException<bool>(ex);
            }
            catch (Exception ex)
            {
                Console.Write("Error when saving user token to the DB");
                return Task.FromException<bool>(ex);
            }
            return Task.FromResult(result);
        }

        /// <summary>
        /// Verifies an admin exists in the "UserAccountRecords" database.
        /// </summary>
        /// <param name="userID"></param>
        /// <returns>  
        ///     True -> If the token that belongs to the userID was removed
        ///     False -> If the token failed to be removed or does not exist. 
        /// </returns>
        public Task<bool> DeleteToken(int userID)
        {
            _connectionString = GetConnectionString();
            int numRows = -1;
            bool result = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                using (SqlCommand command = new SqlCommand("[MEetAndYou].[DeleteUserToken]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@userID", SqlDbType.Int).Value = userID;

                    connection.Open();
                    numRows = command.ExecuteNonQuery();
                    connection.Close();
                }
                if (numRows > 0)
                {
                    result = true;
                }

            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL exception when saving token to the DB!!");
                return Task.FromException<bool>(ex);
            }
            catch (Exception ex)
            {
                Console.Write("Error when saving user token to the DB");
                return Task.FromException<bool>(ex);
            }
            return Task.FromResult(result);
        }
    }
}
