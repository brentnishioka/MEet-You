using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pentaskilled.MEetAndYou.DataAccess.Contracts;
using Pentaskilled.MEetAndYou.Entities;

namespace Pentaskilled.MEetAndYou.DataAccess.Implementation
{
    public class AccountCreationDAO: IAccountCreation 
    {

        // TODO: move this to our config file instead of it being directly in the code.
        private string _connectionString;

        // GetConnectionString() from https://docs.microsoft.com/en-us/dotnet/api/system.data.sqlclient.sqlconnection.connectionstring?view=dotnet-plat-ext-6.0
        static private string GetConnectionString()
        {
            return new ConnectionString().ToString();
        }
        
        public Task<bool> DoesEmailExist(UserAccountEntity user)
        {
            _connectionString = GetConnectionString();
            bool result;

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                using (SqlCommand command = new SqlCommand("[MEetAndYou].[VerifyEmailInDB]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@email", SqlDbType.VarChar).Value = user.Email;

                    connection.Open();
                    result = Convert.ToBoolean(command.ExecuteNonQuery());
                    connection.Close();
                }
            }

            catch (Exception)
            {
                return Task.FromResult(false);
            }
            return Task.FromResult(result);
        }

        /// <summary>
        /// Set account to activated
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<bool> UpdateAccountActivity(UserAccountEntity user)
        {
            _connectionString = GetConnectionString();
            bool result;
            user.Active = 1;
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                using (SqlCommand command = new SqlCommand("[MEetAndYou].[UpdateAccountActive]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@active", SqlDbType.VarChar).Value = user.Active;

                    connection.Open();
                    result = Convert.ToBoolean(command.ExecuteNonQuery());
                    connection.Close();
                }
            }

            catch (Exception)
            {
                return Task.FromResult(false);
            }
            return Task.FromResult(result);
        }

        /// <summary>
        /// Remove account unactivated account after certain period
        /// </summary>
        /// <param name="user">User object</param>
        /// <returns></returns>
        public Task<bool> RemoveUnActivatedAccount(UserAccountEntity user)
        {
             _connectionString = GetConnectionString();
            bool isSuccessfullyDeleted; 

            try
            {
                using SqlConnection connection = new SqlConnection(_connectionString);
                using SqlCommand command = new SqlCommand("[MEetAndYou].[RemoveUnactivatedAccount]", connection);
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@active", SqlDbType.Bit).Value = user.Active;

                    connection.Open();
                    isSuccessfullyDeleted = Convert.ToBoolean(command.ExecuteNonQuery());
                    connection.Close();
                }
                
            }

            catch (Exception)
            {
                return Task.FromResult(false);
            }

            return Task.FromResult(isSuccessfullyDeleted);
        
        }
    }
}

