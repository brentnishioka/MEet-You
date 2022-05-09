using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pentaskilled.MEetAndYou.DataAccess.Contracts;
using Pentaskilled.MEetAndYou.Entities.DBModels;

namespace Pentaskilled.MEetAndYou.DataAccess.Implementation
{
    public class UserDAO : IUserDAO

    {
        private MEetAndYouDBContext _dbContext;
        private string _connectionString;

        static private string GetConnectionString()
        {
            return new ConnectionString().ToString();
        }

        public UserDAO()
        {
            this._dbContext = new MEetAndYouDBContext();
        }

        public UserDAO(MEetAndYouDBContext _dbContext)
        {
            this._dbContext = _dbContext;
        }

        /// <summary>
        /// Method to get user account info from the database
        /// </summary>
        /// <param name="userID"> id of the user</param>
        /// <returns> User account record </returns>
        public async Task<UserAccountRecord> getUserAccount(int userID)
        {
            return await _dbContext.UserAccountRecords.FindAsync(userID);
        }


        public Task<int> GetRegisteredCount(DateTime date)
        {
            _connectionString = GetConnectionString();
            int userCount = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                // First, need to get the user's ID from the DB using their email.
                using (SqlCommand command = new SqlCommand("SELECT [MEetAndYou].[GetRegistrationCount](@@registerDate)", connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add("@registerDate", SqlDbType.VarChar).Value = date;

                    connection.Open();
                    userCount = (int)command.ExecuteScalar();
                    connection.Close();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Sql Exception when querying date using registration date");
                return Task.FromResult(-1);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception when when querying date using registration date");
                return Task.FromResult(-1);
            }
            return Task.FromResult(userCount);
        }
    }
}

