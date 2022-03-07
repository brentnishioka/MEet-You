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

        public bool DoesEmailExist(UserAccountEntity user)
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
                return false;
            }
            return result;
        }

        public bool UpdateAccountActivity(UserAccountEntity user)
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
                return false;
            }
            return result;
        }

        public bool RemoveUnActivedAccount(UserAccountEntity user)
        {
             _connectionString = GetConnectionString();
            bool isSuccessfullyDeleted; 

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                using (SqlCommand command = new SqlCommand("[MEetAndYou].[RemoveUnactivatedAccount]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@email", SqlDbType.VarChar).Value = user.Email;
                    command.Parameters.Add("@active", SqlDbType.Int).Value = user.Active;

                    connection.Open();
                    isSuccessfullyDeleted = Convert.ToBoolean(command.ExecuteNonQuery());
                    connection.Close();
                }
            }

            catch (Exception)
            {
                return false;
            }

            return isSuccessfullyDeleted;
        
        }
    }
}

