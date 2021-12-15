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
        /// Creates a user account record "UserAccountRecords" database.
        /// </summary>
        /// <param name="user"></param>
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
                    command.Parameters.Add("@registerDate", SqlDbType.VarChar).Value = user.RegisterDate;
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

        /// <summary>
        /// Updates a user email in the "UserAccountRecords" database.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newEmail"></param>
        /// <returns>  
        ///     True -> The email is successfully updated in the database.
        ///     False -> The email is not successfully updated in the database.
        /// </returns>
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



        /// <summary>
        /// Updates a user's password in the "UserAccountRecords" in the database.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newPassword"></param>
        /// <returns>  
        ///     True -> The password is successfully updated in the database.
        ///     False -> The password is not successfully updated in the database.
        /// </returns>
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

        /// <summary>
        /// Updates a user's phone number in the "UserAccountRecords" in the database.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newPhoneNum"></param>
        /// <returns>  
        ///     True -> The phone number is successfully updated in the database.
        ///     False -> The phone number is not successfully updated in the database.
        /// </returns>
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

        /// <summary>
        /// Deletes a user in the "UserAccountRecords" in the database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>  
        ///     True -> The user is successfully deleted in the database.
        ///     False -> The user is not successfully deleted in the database.
        /// </returns>
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

        /// <summary>
        /// Disables a user in the "UserAccountRecords" in the database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>  
        ///     True -> The user is successfully disabled.
        ///     False -> The user is not successfully disabled.
        /// </returns>
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

        /// <summary>
        /// Enables a user in the "UserAccountRecords" in the database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>  
        ///     True -> The user is successfully enabled.
        ///     False -> The user is not successfully enabled.
        /// </returns>
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

        /// <summary>
        /// Creates an admin account record "AdminAccountRecords" database.
        /// </summary>
        /// <param name="admin"></param>
        /// <returns>  
        ///     True -> the AdminAccount is inserted into the database successfully.
        ///     False -> the AdminAccount is not successfully inserted into the database.
        /// </returns>
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

        /// <summary>
        /// Updates an admin's email in the "AdminAccountRecords" database.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newEmail"></param>
        /// <returns>  
        ///     True -> The admin's email is successfully updated.
        ///     False -> The admin's email is successfully not updated.
        /// </returns>
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

        /// <summary>
        /// Updates an admin's email in the "AdminAccountRecords" database.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newPassword"></param>
        /// <returns>  
        ///     True -> The admin's password is successfully updated.
        ///     False -> The admin's password is successfully not updated.
        /// </returns>
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

        /// <summary>
        /// Deletes an admin in the "AdminAccountRecords" database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>  
        ///     True -> The admin is successfully deleted.
        ///     False -> The admin is not successfully deleted.
        /// </returns>
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

        /// <summary>
        /// Verifies a user exists in the "UserAccountRecords" database.
        /// </summary>
        /// <param name="user"></param>
        /// <returns>  
        ///     True -> The user exists in the database.
        ///     False -> The user does not exist in the database.
        /// </returns>
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
                    command.Parameters.Add("@id", SqlDbType.Int).Value = user.UserID;
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

        /// <summary>
        /// Verifies an admin exists in the "UserAccountRecords" database.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns>  
        ///     True -> The admin exists in the database.
        ///     False -> The admin does not exist in the database.
        /// </returns>
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
