using Pentaskilled.MEetAndYou.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Pentaskilled.MEetAndYou.DataAccess
{
    public class UMDAO : IUMDAO
    {
        // TODO: move this to our config file instead of it being directly in the code.
        private string _connectionString;

        // GetConnectionString() from https://docs.microsoft.com/en-us/dotnet/api/system.data.sqlclient.sqlconnection.connectionstring?view=dotnet-plat-ext-6.0
        static private string GetConnectionString()
        {
            return @"Data Source=LAPTOP-5VDMOIMK;Initial Catalog=MEetAndYouDB;Integrated Security=True";
        }

        /// <summary>
        /// Create account record into the respective table "AccountRecords" in the database.
        /// </summary>
        /// <param name="ua"></param>
        /// <returns>  
        ///     True -> the UserAccount is inserted into the database successfully.
        ///     False -> the UserAccount is not successfully inserted into the database.
        /// </returns>
        public bool CreateAccountRecord(UserAccountEntity ua)
        {
            _connectionString = GetConnectionString();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                using (SqlCommand command = new SqlCommand("[MEetAndYou].[CreateAccountRecord]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@email", SqlDbType.VarChar).Value = ua.Email;
                    command.Parameters.Add("@password", SqlDbType.VarChar).Value = ua.Password;
                    command.Parameters.Add("@phoneNum", SqlDbType.VarChar).Value = ua.PhoneNumber;
                    command.Parameters.Add("@role", SqlDbType.VarChar).Value = ua.Role;
                    command.Parameters.Add("@registerDate", SqlDbType.DateTime).Value = ua.RegisterDate;
                    command.Parameters.Add("@active", SqlDbType.Bit).Value = ua.Active;

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }

            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public bool UpdateAccountEmail(UserAccountEntity ua, string newEmail)
        {
            _connectionString = GetConnectionString();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                using (SqlCommand command = new SqlCommand("[MEetAndYou].[UpdateAccountEmail]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@id", SqlDbType.Int).Value = ua.UserID;
                    command.Parameters.Add("@email", SqlDbType.VarChar).Value = newEmail;

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }

            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public bool UpdateAccountPassword(UserAccountEntity ua)
        {
            _connectionString = GetConnectionString();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                using (SqlCommand command = new SqlCommand("[MEetAndYou].[UpdateAccountPassword]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@id", SqlDbType.Int).Value = ua.UserID;
                    command.Parameters.Add("@password", SqlDbType.VarChar).Value = ua.Password;

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }

            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public bool UpdateAccountPhone(UserAccountEntity ua)
        {
            _connectionString = GetConnectionString();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                using (SqlCommand command = new SqlCommand("[MEetAndYou].[UpdateAccountPhoneNum]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@id", SqlDbType.Int).Value = ua.UserID;
                    command.Parameters.Add("@phoneNum", SqlDbType.VarChar).Value = ua.PhoneNumber;

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }

            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public bool UpdateAccountRole(UserAccountEntity ua)
        {
            _connectionString = GetConnectionString();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                using (SqlCommand command = new SqlCommand("[MEetAndYou].[UpdateAccountRole]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@id", SqlDbType.Int).Value = ua.UserID;
                    command.Parameters.Add("@role", SqlDbType.VarChar).Value = ua.Role;

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }

            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public bool DeleteAccountRecord(UserAccountEntity ua)
        {
            _connectionString = GetConnectionString();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                using (SqlCommand command = new SqlCommand("[MEetAndYou].[DeleteAccountRecord]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@id", SqlDbType.Int).Value = ua.UserID;

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }

            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public bool DisableAccountRecord(UserAccountEntity ua)
        {
            _connectionString = GetConnectionString();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                using (SqlCommand command = new SqlCommand("[MEetAndYou].[DisableAccountRecord]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@id", SqlDbType.Int).Value = ua.UserID;

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }

            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public bool EnableAccountRecord(UserAccountEntity ua)
        {
            _connectionString = GetConnectionString();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                using (SqlCommand command = new SqlCommand("[MEetAndYou].[EnableAccountRecord]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@id", SqlDbType.Int).Value = ua.UserID;

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }

            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public bool VerifyUserInDB(UserAccountEntity ua)
        {
            _connectionString = GetConnectionString();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                using (SqlCommand command = new SqlCommand("[MEetAndYou].[VerifyUserInDB]", connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add("@id", SqlDbType.Int).Value = ua.UserID;

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }

            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        //public UserAccountEntity GetAccountRecord(int id)
        //{
        //    _connectionString = GetConnectionString();

        //    try
        //    {
        //        using (SqlConnection connection = new SqlConnection(_connectionString))
        //        using (SqlCommand command = new SqlCommand("[MEetAndYou].[GetAccountRecord]", connection))
        //        {
        //            command.CommandType = CommandType.Text;
        //            command.Parameters.Add("@id", SqlDbType.Int).Value = id;

        //            connection.Open();
        //            command.ExecuteNonQuery();
        //            connection.Close();
        //        }
        //    }

        //    catch (Exception ex)
        //    {
        //        return;
        //    }
        //    return true;
        //}
    }
}
