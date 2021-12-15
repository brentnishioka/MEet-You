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
       
        public bool isUserCreated(UserAccountEntity user)
        {
            _connectionString = GetConnectionString();
            bool isSuccessfullyCreated; 

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
                    isSuccessfullyCreated = Convert.ToBoolean(command.ExecuteNonQuery());
                    connection.Close();
                }
            }

            catch (Exception)
            {
                return false;
            }
            return isSuccessfullyCreated;
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
        public bool isUserEmailUpdated(int id, string newEmail)
        {
            _connectionString = GetConnectionString();
            bool isSuccessfullyUpdated; 

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                using (SqlCommand command = new SqlCommand("[MEetAndYou].[UpdateUserAccountEmail]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    command.Parameters.Add("@email", SqlDbType.VarChar).Value = newEmail;

                    connection.Open();
                    isSuccessfullyUpdated = Convert.ToBoolean(command.ExecuteNonQuery());
                    connection.Close();
                }
            }

            catch (Exception)
            {
                return false;
            }
            return isSuccessfullyUpdated;
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
        public bool isUserPasswordUpdated(int id, string newPassword)
        {
            _connectionString = GetConnectionString();
            bool isSuccessfullyUpdated; 

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                using (SqlCommand command = new SqlCommand("[MEetAndYou].[UpdateUserAccountPassword]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    command.Parameters.Add("@password", SqlDbType.VarChar).Value = newPassword;

                    connection.Open();
                    isSuccessfullyUpdated = Convert.ToBoolean(command.ExecuteNonQuery());
                    connection.Close();
                }
            }

            catch (Exception)
            {
                return false;
            }
            return isSuccessfullyUpdated;
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
        public bool isUserPhoneUpdated(int id, string newPhoneNum)
        {
            _connectionString = GetConnectionString();
            bool isSuccessfullyUpdated; 

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                using (SqlCommand command = new SqlCommand("[MEetAndYou].[UpdateUserAccountPhoneNum]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    command.Parameters.Add("@phoneNum", SqlDbType.VarChar).Value = newPhoneNum;

                    connection.Open();
                    isSuccessfullyUpdated = Convert.ToBoolean(command.ExecuteNonQuery());
                    connection.Close();
                }
            }

            catch (Exception)
            {
                return false;
            }
            return isSuccessfullyUpdated;
        }

        /// <summary>
        /// Deletes a user in the "UserAccountRecords" in the database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>  
        ///     True -> The user is successfully deleted in the database.
        ///     False -> The user is not successfully deleted in the database.
        /// </returns>
        public bool isUserDeleted(int id)
        {
            _connectionString = GetConnectionString();
            bool isSuccessfullyDeleted; 

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                using (SqlCommand command = new SqlCommand("[MEetAndYou].[DeleteUserAccountRecord]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;

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

        /// <summary>
        /// Disables a user in the "UserAccountRecords" in the database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>  
        ///     True -> The user is successfully disabled.
        ///     False -> The user is not successfully disabled.
        /// </returns>
        public bool isUserDisabled(int id)
        {
            _connectionString = GetConnectionString();
            bool isSuccessfullyDisabled;

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                using (SqlCommand command = new SqlCommand("[MEetAndYou].[DisableUserAccountRecord]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;

                    connection.Open();
                    isSuccessfullyDisabled = Convert.ToBoolean(command.ExecuteNonQuery());
                    connection.Close();
                }
            }

            catch (Exception)
            {
                return false;
            }

            return isSuccessfullyDisabled;
        }

        /// <summary>
        /// Enables a user in the "UserAccountRecords" in the database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>  
        ///     True -> The user is successfully enabled.
        ///     False -> The user is not successfully enabled.
        /// </returns>
        public bool isUserEnabled(int id)
        {
            _connectionString = GetConnectionString();
            bool isSuccessfullyEnabled;

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                using (SqlCommand command = new SqlCommand("[MEetAndYou].[EnableUserAccountRecord]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;

                    connection.Open();
                    isSuccessfullyEnabled = Convert.ToBoolean(command.ExecuteNonQuery());
                    connection.Close();
                }
            }

            catch (Exception)
            {
                return false;
            }
            return isSuccessfullyEnabled;
        }

        /// <summary>
        /// Creates an admin account record "AdminAccountRecords" database.
        /// </summary>
        /// <param name="admin"></param>
        /// <returns>  
        ///     True -> the AdminAccount is inserted into the database successfully.
        ///     False -> the AdminAccount is not successfully inserted into the database.
        /// </returns>
        public bool isAdminCreated(AdminAccountEntity admin)
        {
            _connectionString = GetConnectionString();
            bool isSuccessfullyCreated;

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                using (SqlCommand command = new SqlCommand("[MEetAndYou].[CreateAdminAccountRecord]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@email", SqlDbType.VarChar).Value = admin.Email;
                    command.Parameters.Add("@password", SqlDbType.VarChar).Value = admin.Password;

                    connection.Open();
                    isSuccessfullyCreated = Convert.ToBoolean(command.ExecuteNonQuery());
                    connection.Close();
                }
            }

            catch (Exception)
            {
                return false;
            }
            return isSuccessfullyCreated;
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
        public bool isAdminEmailUpdated(int id, string newEmail)
        {
            _connectionString = GetConnectionString();
            bool isSuccessfullyUpdated;

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                using (SqlCommand command = new SqlCommand("[MEetAndYou].[UpdateAdminAccountEmail]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    command.Parameters.Add("@email", SqlDbType.VarChar).Value = newEmail;

                    connection.Open();
                    isSuccessfullyUpdated = Convert.ToBoolean(command.ExecuteNonQuery());
                    connection.Close();
                }
            }

            catch (Exception)
            {
                return false;
            }
            return isSuccessfullyUpdated;
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
        public bool isAdminPasswordUpdated(int id, string newPassword)
        {
            _connectionString = GetConnectionString();
            bool isSuccessfullyUpdated;

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                using (SqlCommand command = new SqlCommand("[MEetAndYou].[UpdateAdminAccountPassword]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    command.Parameters.Add("@password", SqlDbType.VarChar).Value = newPassword;

                    connection.Open();
                    isSuccessfullyUpdated = Convert.ToBoolean(command.ExecuteNonQuery());
                    connection.Close();
                }
            }

            catch (Exception)
            {
                return false;
            }
            return isSuccessfullyUpdated;
        }

        /// <summary>
        /// Deletes an admin in the "AdminAccountRecords" database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>  
        ///     True -> The admin is successfully deleted.
        ///     False -> The admin is not successfully deleted.
        /// </returns>
        public bool isAdminDeleted(int id)
        {
            _connectionString = GetConnectionString();
            bool isSuccessfullyDeleted;

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                using (SqlCommand command = new SqlCommand("[MEetAndYou].[DeleteAdminAccountRecord]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;

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

        /// <summary>
        /// Verifies a user exists in the "UserAccountRecords" database.
        /// </summary>
        /// <param name="user"></param>
        /// <returns>  
        ///     True -> The user exists in the database.
        ///     False -> The user does not exist in the database.
        /// </returns>
        public bool isUserInDBVerified(UserAccountEntity user)
        {
            _connectionString = GetConnectionString();
            int rowsAffected;

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
                    rowsAffected = (int) command.ExecuteScalar();
                    connection.Close();
                }
            }

            catch (Exception)
            {
                return false;
            }
            return Convert.ToBoolean(rowsAffected);
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
        public bool isAdminInDBVerified(string email, string password)
        {
            _connectionString = GetConnectionString();
            int isAdminVerified;

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                using (SqlCommand command = new SqlCommand("SELECT [MEetAndYou].[VerifyAdminRecordInDB] (@email, @password)", connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add("@email", SqlDbType.NVarChar).Value = email;
                    command.Parameters.Add("@password", SqlDbType.NVarChar).Value = password;

                    connection.Open();
                    isAdminVerified = (int)command.ExecuteScalar();
                    connection.Close();
                }
            }

            catch (Exception)
            {
                return false;
            }
            return Convert.ToBoolean(isAdminVerified);
        }
    }
}
