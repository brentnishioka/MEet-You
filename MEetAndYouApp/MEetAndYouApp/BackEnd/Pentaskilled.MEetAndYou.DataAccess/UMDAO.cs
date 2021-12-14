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
            return @"Data Source=localhost;Initial Catalog=MEetAndYouDB;Integrated Security=True";
        }

        /// <summary>
        /// Create account record into the respective table "AccountRecords" in the database.
        /// </summary>
        /// <param name="ua"></param>
        /// <returns>  
        ///     True -> the UserAccount is inserted into the database successfully.
        ///     False -> the UserAccount is not successfully inserted into the database.
        /// </returns>
        public bool CreateUserAccountRecord(UserAccountEntity user)
        {
            _connectionString = GetConnectionString();
            bool result; 

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                using (SqlCommand command = new SqlCommand("[MEetAndYou].[CreateUserAccountRecord]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@email", SqlDbType.VarChar).Value = user.Email;
                    command.Parameters.Add("@password", SqlDbType.VarChar).Value = user.Password;
                    command.Parameters.Add("@phoneNum", SqlDbType.VarChar).Value = user.PhoneNumber;
                    command.Parameters.Add("@registerDate", SqlDbType.DateTime).Value = user.RegisterDate;
                    command.Parameters.Add("@active", SqlDbType.Bit).Value = user.Active;

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

        public bool UpdateUserAccountEmail(int id, string newEmail)
        {
            _connectionString = GetConnectionString();
            bool result; 

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                using (SqlCommand command = new SqlCommand("[MEetAndYou].[UpdateUserAccountEmail]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    command.Parameters.Add("@email", SqlDbType.VarChar).Value = newEmail;

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

        public bool UpdateUserAccountPassword(int id, string newPassword)
        {
            _connectionString = GetConnectionString();
            bool result; 

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                using (SqlCommand command = new SqlCommand("[MEetAndYou].[UpdateUserAccountPassword]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    command.Parameters.Add("@password", SqlDbType.VarChar).Value = newPassword;

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

        public bool UpdateUserAccountPhone(int id, string newPhoneNum)
        {
            _connectionString = GetConnectionString();
            bool result; 

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                using (SqlCommand command = new SqlCommand("[MEetAndYou].[UpdateUserAccountPhoneNum]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    command.Parameters.Add("@phoneNum", SqlDbType.VarChar).Value = newPhoneNum;

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

        public bool DeleteUserAccountRecord(int id)
        {
            _connectionString = GetConnectionString();
            bool result; 

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                using (SqlCommand command = new SqlCommand("[MEetAndYou].[DeleteUserAccountRecord]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;

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

        public bool DisableUserAccountRecord(int id)
        {
            _connectionString = GetConnectionString();
            bool result;

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                using (SqlCommand command = new SqlCommand("[MEetAndYou].[DisableUserAccountRecord]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;

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

        public bool EnableUserAccountRecord(int id)
        {
            _connectionString = GetConnectionString();
            bool result;

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                using (SqlCommand command = new SqlCommand("[MEetAndYou].[EnableUserAccountRecord]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;

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

        public bool CreateAdminAccountRecord(AdminAccountEntity admin)
        {
            _connectionString = GetConnectionString();
            bool result;

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                using (SqlCommand command = new SqlCommand("[MEetAndYou].[CreateAdminAccountRecord]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@email", SqlDbType.VarChar).Value = admin.Email;
                    command.Parameters.Add("@password", SqlDbType.VarChar).Value = admin.Password;

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

        public bool UpdateAdminAccountEmail(int id, string newEmail)
        {
            _connectionString = GetConnectionString();
            bool result;

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                using (SqlCommand command = new SqlCommand("[MEetAndYou].[UpdateAdminAccountEmail]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    command.Parameters.Add("@email", SqlDbType.VarChar).Value = newEmail;

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

        public bool UpdateAdminAccountPassword(int id, string newPassword)
        {
            _connectionString = GetConnectionString();
            bool result;

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                using (SqlCommand command = new SqlCommand("[MEetAndYou].[UpdateAdminAccountPassword]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    command.Parameters.Add("@password", SqlDbType.VarChar).Value = newPassword;

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

        public bool DeleteAdminAccountRecord(int id)
        {
            _connectionString = GetConnectionString();
            bool result;

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                using (SqlCommand command = new SqlCommand("[MEetAndYou].[DeleteAdminAccountRecord]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;

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


        public bool VerifyUserRecordInDB(UserAccountEntity user)
        {
            _connectionString = GetConnectionString();
            int result;

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                using (SqlCommand command = new SqlCommand("SELECT [MEetAndYou].[VerifyUserRecordInDB] (@id, @email, @password, @phoneNum, @registerDate, @active)", connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add("@email", SqlDbType.VarChar).Value = user.Email;
                    command.Parameters.Add("@password", SqlDbType.VarChar).Value = user.Password;
                    command.Parameters.Add("@phoneNum", SqlDbType.VarChar).Value = user.PhoneNumber;
                    command.Parameters.Add("@registerDate", SqlDbType.DateTime).Value = user.RegisterDate;
                    command.Parameters.Add("@active", SqlDbType.Bit).Value = user.Active;

                    connection.Open();
                    result = (int) command.ExecuteScalar();
                    connection.Close();
                }
            }

            catch (Exception)
            {
                return false;
            }
            return Convert.ToBoolean(result);
        }

        public bool VerifyAdminRecordInDB(string email, string password)
        {
            _connectionString = GetConnectionString();
            int result;

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                using (SqlCommand command = new SqlCommand("SELECT [MEetAndYou].[VerifyAdminRecordInDB] (@email, @password)", connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add("@email", SqlDbType.NVarChar).Value = email;
                    command.Parameters.Add("@password", SqlDbType.NVarChar).Value = password;

                    connection.Open();
                    result = (int)command.ExecuteScalar();
                    connection.Close();
                }
            }

            catch (Exception)
            {
                return false;
            }
            return Convert.ToBoolean(result);
        }
    }
}
