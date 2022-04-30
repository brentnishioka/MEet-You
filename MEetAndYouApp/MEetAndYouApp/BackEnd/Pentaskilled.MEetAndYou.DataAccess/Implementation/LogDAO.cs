using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Pentaskilled.MEetAndYou.Entities;

namespace Pentaskilled.MEetAndYou.DataAccess
{
    public class LogDAO : ILogDAO
    {
        // TODO: move this to our config file instead of it being directly in the code.
        private string _connectionString;

        // GetConnectionString() from https://docs.microsoft.com/en-us/dotnet/api/system.data.sqlclient.sqlconnection.connectionstring?view=dotnet-plat-ext-6.0
        static private string GetConnectionString()
        {
            return new ConnectionString().ToString();
        }

        /// <summary>
        /// Inserts logs into the database table "EventLogs" asynchronously.
        /// </summary>
        /// <param name="eventLog"> The data transfer object (DTO) containing all our log information. </param>
        /// <returns>
        /// Returns true if the log is inserted into the database successfully, false if otherwise.
        /// </returns>
        public async Task<bool> PushLogToDBAsync(Log eventLog)
        {
            return await Task.Run(() => {
                _connectionString = GetConnectionString();
                Enum logLvl = eventLog.logLevel;
                try
                {
                    using (SqlConnection connection = new SqlConnection(_connectionString))
                    using (SqlCommand command = new SqlCommand("[MEetAndYou].[InsertLog]", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@dateTime", SqlDbType.DateTime).Value = eventLog.dateTime;
                        command.Parameters.Add("@category", SqlDbType.VarChar).Value = eventLog.category;
                        command.Parameters.Add("@logLevel", SqlDbType.VarChar).Value = logLvl.ToString();
                        command.Parameters.Add("@userId", SqlDbType.Int).Value = eventLog.userId;
                        command.Parameters.Add("@message", SqlDbType.VarChar).Value = eventLog.message;

                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
                catch (Exception)
                {
                    return false;
                }
                return true;
            });
        }

        /// <summary>
        /// Retrieves the last logId value from our "EventLogs" database table asynchronously.
        /// </summary>
        /// <returns> Returns the ID of the last log currently in our "EventLogs" table. </returns>
        /// <exception cref="NullReferenceException"> Exception thrown for if null data is returned from our database function. </exception>
        /// <exception cref="Exception"> Handles all other exceptions. </exception>
        public async Task<int> GetCurrentIdentityAsync()
        {
            return await Task.Run(() => {
                int lastLogId = 0;
                _connectionString = GetConnectionString();
                try
                {
                    using (SqlConnection connection = new SqlConnection(_connectionString))
                    {
                        SqlCommand command = new SqlCommand("SELECT [MEetAndYou].[GetCurrentIdentity]()", connection);
                        connection.Open();
                        lastLogId = (int)command.ExecuteScalar();
                        connection.Close();
                    }
                }
                catch (NullReferenceException)
                {
                    throw new NullReferenceException("ERROR: Null data returned from the function GetCurrentIdentity.");
                }
                catch (Exception)
                {
                    throw new Exception();
                }
                return lastLogId;
            });
        }

        /// <summary>
        /// Validates whether the given log currently exists in the "EventLogs" database table asynchronously.
        /// </summary>
        /// <param name="eventLog"> The data transfer object (DTO) containing all our log information. </param>
        /// <returns> Returns a count of existing logs with the provided eventLog's ID in the database. </returns>
        /// <exception cref="NullReferenceException"> Exception thrown for if null data is returned from our database function. </exception>
        /// <exception cref="Exception"> Handles all other exceptions. </exception>
        public async Task<int> CheckExistingLogAsync(Log eventLog)
        {
            return await Task.Run(() => {
                int numRows = 0;
                _connectionString = GetConnectionString();
                try
                {
                    using (SqlConnection connection = new SqlConnection(_connectionString))
                    {
                        SqlCommand command = new SqlCommand("SELECT [MEetAndYou].[CheckExistingLog](@logId)", connection);
                        command.Parameters.Add("@logId", SqlDbType.Int).Value = eventLog.logId;
                        connection.Open();
                        numRows = (int)command.ExecuteScalar();
                        connection.Close();
                    }
                }
                catch (NullReferenceException)
                {
                    throw new NullReferenceException("ERROR: Null data returned from the function GetCurrentIdentity.");
                }
                catch (Exception)
                {
                    throw new Exception();
                }
                return numRows;
            });
        }

        /// <summary>
        /// Updates the given log in the "EventLogs" database table asynchronously.
        /// </summary>
        /// <param name="eventLog"> The data transfer object (DTO) containing all our log information. </param>
        /// <returns> 
        /// Returns null if the log failed to update, or returns the provided log if it was 
        /// updated in the database. 
        /// </returns>
        public async Task<Log> UpdateLogAsync(Log eventLog)
        {
            return await Task.Run(() => {
                _connectionString = GetConnectionString();
                try
                {
                    using (SqlConnection connection = new SqlConnection(_connectionString))
                    using (SqlCommand command = new SqlCommand("[MEetAndYou].[UpdateLog]", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@sysLogId", SqlDbType.Int).Value = eventLog.logId;
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
                catch (Exception)
                {
                    return null;
                }
                return eventLog;
            });
        }

        /// <summary>
        /// Gets all the logs older than 30 days based on the user's local time.
        /// </summary>
        /// <returns> 
        /// Returns null if there are no log entries in our database older than 30 days,
        /// otherwise, it returns a list of those logs.
        /// </returns>
        public List<Log> ReadLogsOlderThan30()
        {
            _connectionString = GetConnectionString();
            List<Log> logs30DayOlder = new List<Log>();
            Log tempEventLog;
            LogLevelDict logLvl = new LogLevelDict();
            Dictionary<string, LogLevel> dict = logLvl.logLvlDict;
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    SqlCommand oldEventLogsCommand = new SqlCommand("SELECT * FROM [MEetAndYou].[Logs30DaysOld]()", connection);

                    connection.Open();
                    SqlDataReader reader = oldEventLogsCommand.ExecuteReader();
                    while (reader.Read())
                    {
                        tempEventLog = new Log();
                        tempEventLog.logId = Convert.ToInt32(reader[0]);
                        tempEventLog.dateTime = Convert.ToDateTime(reader[1]);
                        tempEventLog.category = Convert.ToString(reader[2]);
                        tempEventLog.logLevel = dict[Convert.ToString(reader[3])];
                        tempEventLog.userId = Convert.ToInt32(reader[4]);
                        tempEventLog.message = Convert.ToString(reader[5]);

                        logs30DayOlder.Add(tempEventLog);
                    }
                    reader.Close();
                    connection.Close();
                }
            }
            catch (Exception)
            {
                return null;
            }

            return logs30DayOlder;
        }

        /// <summary>
        /// Removes all the logs older than 30 days from the "EventLogs" database table.
        /// </summary>
        /// <returns> Returns true if the logs older than 30 days are deleted from the database, false if otherwise. </returns>
        public bool DeleteLogsOlderThan30()
        {
            _connectionString = GetConnectionString();
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                using (SqlCommand newSysLogsCommand = new SqlCommand("[MEetAndYou].[ArchiveDelete]", connection))
                {
                    newSysLogsCommand.CommandType = CommandType.StoredProcedure;

                    connection.Open();
                    newSysLogsCommand.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Retrieves the number of logs older than 30 days from the "EventLogs" database table.
        /// </summary>
        /// <returns> Returns the number of logs older than 30 days. </returns>
        /// <exception cref="NullReferenceException"> Exception thrown for if null data is returned from our database function. </exception>
        /// <exception cref="Exception"> Handles all other exceptions. </exception>
        public int GetArchiveCount()
        {
            _connectionString = GetConnectionString();
            int count;
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    SqlCommand command = new SqlCommand("SELECT [MEetAndYou].[GetArchiveCount]()", connection);
                    connection.Open();
                    count = (int)command.ExecuteScalar();
                    connection.Close();
                }
            }
            catch (NullReferenceException)
            {
                throw new NullReferenceException("ERROR: Null data returned from the function GetArchiveCount.");

            }
            catch (Exception)
            {
                throw new Exception();
            }
            return count;
        }
    }
}
