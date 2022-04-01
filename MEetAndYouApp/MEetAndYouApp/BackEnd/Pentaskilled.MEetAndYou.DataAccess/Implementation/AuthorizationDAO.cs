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

        public string ConnectionString { get { return _connectionString; } set { _connectionString = value; } }

        // Fix this please, the commands does not know the correct table and columns
        /// <summary>
        /// Verify token takes in the string and the userID to verify if the user token is the same one 
        /// with the one in UserToken Table
        /// </summary>
        /// <param name="userID">the ID of the user with the token.</param>
        /// <param name="token">the string token that is used to get the hash.</param>
        /// <returns>Return true if token exist in Database, False if not..</returns>
        public bool VerifyToken(int userID, string token)
        {
            _connectionString = GetConnectionString();
            bool result = false;      // User ID column to be read from    
            int rowsAffected;
            string currentTime = DateTime.Now.ToString("yyyy-mm-dd");

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
                Console.WriteLine("SQL exception when verifying a token. " + "\n" + ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception when verifying a token. " + "\n" + ex.Message);
                return false;
            }

            return result;
        }


        // <params> token is not hashed, the DB will hashed this string to compare with the one in the database. 
        public List<string> GetRoles(int userID)
        {
            //throw new NotImplementedException();
            _connectionString = GetConnectionString();
            SqlDataReader reader;
            List<string> roles = new List<string>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                using (SqlCommand command = new SqlCommand("select * from MEetAndYou.GetRolesByID(@UserID)", connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add("@UserID", SqlDbType.VarChar).Value = userID;

                    connection.Open();
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        string role = reader.GetString(0);
                        roles.Add(role);
                    }
                    connection.Close();

                }
            }
            // Change this so that it signify a failure
            catch (SqlException ex)
            {
                Console.WriteLine("SQL error when querying data" + "\n" + ex.Message);
                return new List<string>();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error when getting user role" + "\n" + ex.Message);
                return new List<string>();
            }

            return roles;
        }

        public Task<List<string>> GetAllRoles()
        {
            //throw new NotImplementedException();
            _connectionString = GetConnectionString();
            SqlDataReader reader;
            int userIDCol = 1;      // User ID column to be read from    
            List<string> roles = new List<string>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                using (SqlCommand command = new SqlCommand("select * from MEetAndYou.UserRole;", connection))
                {
                    //command.CommandType = CommandType.Text;
                    //command.Parameters.Add("@UserID", SqlDbType.VarChar).Value = userID;
                    Console.WriteLine("We got here at least");
                    connection.Open();
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Console.WriteLine(reader.GetValue(0));
                        Console.WriteLine("role" + reader.GetValue(1));
                        string role = (string)reader.GetString(1);
                        roles.Add(role);
                    }
                    connection.Close();
                }

            }
            catch (SqlException ex)
            {
                throw;
            }
            // Change this so that it signify a failure
            catch (Exception ex)
            {
                return Task.FromResult(new List<string>());
            }

            return Task.FromResult(roles);
        }

        // Async versin of the VerifyToken
        //public async Task<bool> VerifyTokenAsync(int userID, string token)
        //{
        //    _connectionString = GetConnectionString();
        //    bool result = false;      // User ID column to be read from    
        //    int rowsAffected;

        //    try
        //    {
        //        using (SqlConnection connection = new SqlConnection(_connectionString))
        //        // Call a procedure in the DB to compare the unhashed token with a hashed token. 
        //        using (SqlCommand command = new SqlCommand("EXEC [MEetAndYou].[VerifyUserToken](@userID, @token)", connection))
        //        {
        //            command.CommandType = CommandType.Text;
        //            command.Parameters.Add("@token", SqlDbType.VarChar).Value = token;
        //            command.Parameters.Add("@userID", SqlDbType.Int).Value = userID;

        //            connection.Open();
        //            //reader = command.ExecuteReader();
        //            var rowAsyncTask = await command.ExecuteScalarAsync();
        //            rowsAffected = (int) rowAsyncTask;
        //            connection.Close();
        //        }
        //        //userID = reader.GetFieldValue<int>(userIDCol);

        //        if (rowsAffected > 0)
        //        {
        //            result = true;
        //        }

        //    }
        //    catch (SqlException ex)
        //    {
        //        return false;
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }

        //    return result;
        //}

        //public async Task<List<string>> GetRolesAsync(int userID)
        //{
        //    //throw new NotImplementedException();
        //    _connectionString = GetConnectionString();
        //    SqlDataReader reader;
        //    List<string> roles = new List<string>();

        //    try
        //    {
        //        using (SqlConnection connection = new SqlConnection(_connectionString))
        //        using (SqlCommand command = new SqlCommand("select * from MEetAndYou.GetRolesByID(@UserID)", connection))
        //        {
        //            command.CommandType = CommandType.Text;
        //            command.Parameters.Add("@UserID", SqlDbType.VarChar).Value = userID;

        //            connection.Open();
        //            reader = await command.ExecuteReaderAsync();
        //            while (reader.Read())
        //            {
        //                string role = reader.GetString(0);
        //                roles.Add(role);
        //            }
        //            connection.Close();

        //        }
        //    }
        //    // Change this so that it signify a failure
        //    catch (SqlException ex)
        //    {
        //        Console.WriteLine("SQL error when querying data" + "\n" + ex.Message);
        //        return new List<string>();
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Error when getting user role" + "\n" + ex.Message);
        //        return new List<string>();
        //    }

        //    return roles;
        //}
    }
}
