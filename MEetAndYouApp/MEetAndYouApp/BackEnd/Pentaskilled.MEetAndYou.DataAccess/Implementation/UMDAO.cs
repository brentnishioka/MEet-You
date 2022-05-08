using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pentaskilled.MEetAndYou.Entities;
using Pentaskilled.MEetAndYou.Entities.DBModels;
using Pentaskilled.MEetAndYou.Entities.Models;

namespace Pentaskilled.MEetAndYou.DataAccess
{
    public class UMDAO : IUMDAO
    {
        // TODO: move this to our config file instead of it being directly in the code.
        private string _connectionString;
        private readonly MEetAndYouDBContext _dbContext;

        public UMDAO()
        {
            _dbContext = new MEetAndYouDBContext();
        }

        public UMDAO(MEetAndYouDBContext dbContext)
        {
            _dbContext = dbContext;
        }

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

        public async Task<BaseResponse> DeleteAccAsync(UserAccountEntity userAcc)
        {
            int userID = userAcc.UserID;
            List<Itinerary> userItineraries;
            List<EventLog> userLogs;

            try
            {
                // Find all of the user's itineraries
                userItineraries = await
                    (from itin in _dbContext.Itineraries.Include("Events")
                    where itin.ItineraryOwner == userID
                    select itin).ToListAsync<Itinerary>();

                // Loop through each itinerary and delete the information in all tables
                foreach (Itinerary itin in userItineraries)
                {
                    // Get the current itinerary's ID
                    int itineraryID = itin.ItineraryId;

                    // Retrieve all entries for given itinerary ID from the UserItinerary table to be deleted.
                    _dbContext.UserItineraries.RemoveRange(_dbContext.UserItineraries.Where(x => x.ItineraryId == itineraryID));

                    // Retrieve all images for given itinerary ID from the UserItinerary table to be deleted.
                    _dbContext.Images.RemoveRange(_dbContext.Images.Where(x => x.ItineraryId == itineraryID));

                    // Retrieve all user event ratings for given itinerary ID from the UserItinerary table to be deleted.
                    _dbContext.UserEventRatings.RemoveRange(_dbContext.UserEventRatings.Where(x => x.ItineraryId == itineraryID));

                    // Retrieve all itinerary note for given itinerary ID from the UserItinerary table to be deleted.
                    _dbContext.ItineraryNotes.RemoveRange(_dbContext.ItineraryNotes.Where(x => x.ItineraryId == itineraryID));

                    // Delete all events from an itinerary
                    foreach (Event userEvent in itin.Events)
                    {
                        //Find the Event
                        Event e = await _dbContext.Events.FindAsync(userEvent.EventId);
                        //Console.Write("EventID: " + e.EventId + " " + "Event Name: " + e.EventName);

                        //Remove event from the itinerary
                        itin.Events.Remove(e);
                        _dbContext.Entry(e).State = EntityState.Deleted;
                    }

                    // Removes the given itinerary
                    _dbContext.Itineraries.Remove(itin);
                    _dbContext.Entry(itin).State = EntityState.Deleted;
                }

                // Retrieve all user event logs from logs table to be deleted.
                _dbContext.EventLogs.RemoveRange(_dbContext.EventLogs.Where(x => x.UserId == userID));

                // Find the user account record in the database
                UserAccountRecord currentUser = await _dbContext.UserAccountRecords.FindAsync(userID);

                // Remove any roles the current user has to our system
                _dbContext.Roles.RemoveRange(_dbContext.Roles.Where(x => x.Users == currentUser));

                // Remove the current user from the database
                _dbContext.UserAccountRecords.Remove(currentUser);
                _dbContext.Entry(currentUser).State = EntityState.Deleted;

                // Gets the token assocaited with given user ID
                UserToken uToken = await 
                    (from token in _dbContext.UserTokens
                     where token.UserId == userID
                     select token).FirstOrDefaultAsync<UserToken>();

                // Removes the token from the database
                _dbContext.UserTokens.Remove(uToken);
                _dbContext.Entry(uToken).State = EntityState.Deleted;

                int result = await _dbContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                return new BaseResponse("The user's account could not be deleted.", false);
            }
            return new BaseResponse("The user's account was successfully deleted.", true);
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
