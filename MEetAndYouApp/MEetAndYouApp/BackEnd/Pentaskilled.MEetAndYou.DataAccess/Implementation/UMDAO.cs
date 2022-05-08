﻿using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Pentaskilled.MEetAndYou.Entities;

namespace Pentaskilled.MEetAndYou.DataAccess
{
    public class UMDAO : IUMDAO
    {
        // TODO: move this to our config file instead of it being directly in the code.
        private string _connectionString;

        // GetConnectionString() from https://docs.microsoft.com/en-us/dotnet/api/system.data.sqlclient.sqlconnection.connectionstring?view=dotnet-plat-ext-6.0
        static private string GetConnectionString()
        {
            return new ConnectionString().ToString();
        }

        /// <summary>
        /// Creates a user account record "UserAccountRecords" database.
        /// </summary>
        /// <param name="user"></param>
        /// <returns>  
        ///     True -> the UserAccount is inserted into the database successfully.
        ///     False -> the UserAccount is not successfully inserted into the database.
        /// </returns>

        public bool IsUserCreated(UserAccountEntity user)
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
        public bool IsUserEmailUpdated(int id, string newEmail)
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
        public bool IsUserPasswordUpdated(int id, string newPassword)
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
        public bool IsUserPhoneUpdated(int id, string newPhoneNum)
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
        public bool IsUserDeleted(int id)
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

        public Task<bool> DeleteAcc(UserAccountEntity userAcc)
        {
            _connectionString = GetConnectionString();
            int userID;
            bool isUserDel;
            bool isRemovedFromUserRoles;

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                // First, need to get the user's ID from the DB using their email.
                using (SqlCommand command = new SqlCommand("SELECT [MEetAndYou].[GetUserID](@email)", connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add("@email", SqlDbType.VarChar).Value = userAcc.Email;

                    connection.Open();
                    userID = (int)command.ExecuteScalar();
                    connection.Close();
                }

                if (userID == null)
                {
                    throw new Exception();
                }

                // Then, we need to delete it from our UserRoles table.
                using (SqlConnection connection = new SqlConnection(_connectionString))
                using (SqlCommand command = new SqlCommand("[MEetAndYou].[DeleteUserFromUserRoles]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@id", SqlDbType.Int).Value = userID;

                    connection.Open();
                    isRemovedFromUserRoles = Convert.ToBoolean(command.ExecuteNonQuery());
                    connection.Close();
                }

                // Error check to see if the user was successfully removed from the roles table.
                if (!isRemovedFromUserRoles)
                {
                    throw new Exception();
                }

                // Then, take that ID and delete from the database.
                isUserDel = IsUserDeleted(userID);

                // Error check to see if the user was successfully removed from the user accounts table.
                if (!isUserDel)
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                return Task.FromResult(false);
            }

            return Task.FromResult(Convert.ToBoolean(isUserDel));
        }

        /// <summary>
        /// Get a userID using user emamil in the "UserAccountRecords" in the database.
        /// </summary>
        /// <param name="userEmail"></param>
        /// <returns>  
        ///     userID > 0 if the user is the datbase
        ///     userID = -1 if the user is not found
        /// </returns>
        public Task<int> GetUserIDByEmail(string userEmail)
        {
            _connectionString = GetConnectionString();
            int userID = -1;

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                // First, need to get the user's ID from the DB using their email.
                using (SqlCommand command = new SqlCommand("SELECT [MEetAndYou].[GetUserID](@email)", connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add("@email", SqlDbType.VarChar).Value = userEmail;

                    connection.Open();
                    userID = (int)command.ExecuteScalar();
                    connection.Close();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Sql Exception when querying for userID using email.");
                return Task.FromResult(-1);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception when querying for userID using email.");
                return Task.FromResult(-1);
            }
            return Task.FromResult(userID);
        }

        /// <summary>
        /// Disables a user in the "UserAccountRecords" in the database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>  
        ///     True -> The user is successfully disabled.
        ///     False -> The user is not successfully disabled.
        /// </returns>
        public bool IsUserDisabled(int id)
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
        public bool IsUserEnabled(int id)
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
        public bool IsAdminCreated(AdminAccountEntity admin)
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
        public bool IsAdminEmailUpdated(int id, string newEmail)
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
        public bool IsAdminPasswordUpdated(int id, string newPassword)
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
        public bool IsAdminDeleted(int id)
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
        public bool IsUserInDBVerified(UserAccountEntity user)
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
                    rowsAffected = (int)command.ExecuteScalar();
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
        public bool IsAdminInDBVerified(string email, string password)
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
