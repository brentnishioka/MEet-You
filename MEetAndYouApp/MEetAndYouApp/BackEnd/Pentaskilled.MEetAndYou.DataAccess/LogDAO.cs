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
    public class LogDAO : ILogDAO
    {
        // TODO: move this to our config file instead of it being directly in the code.
        private string _connectionString;

        // GetConnectionString() from https://docs.microsoft.com/en-us/dotnet/api/system.data.sqlclient.sqlconnection.connectionstring?view=dotnet-plat-ext-6.0
        static private string GetConnectionString()
        { 
            return @"Data Source=localhost;Initial Catalog=MEetAndYou-DB;Integrated Security=True";
        }

        /// <summary>
        /// Inserts logs into the respective table "EventLogs" in the database.
        /// </summary>
        /// <param name="eventLog"> The data transfer object (DTO) of all our log information. </param>
        /// <returns>
        ///     True -> the log is inserted into the database successfully.
        ///     False -> the log is not successfully inserted into the database.
        /// </returns>
        public bool PushLogToDB(Log eventLog)
        {
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
        }

        public int GetCurrentIdentity()
        {
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
            catch (Exception ex)
            {
                throw new NullReferenceException();
            }
            return lastLogId;
        }

        public int CheckExistingLog(Log eventLog)
        {
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
            catch (Exception ex)
            {
                throw new NullReferenceException();
            }
            return numRows;
        }

        public Log UpdateLog(Log eventLog)
        {
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
            catch (Exception ex)
            {
                return null;
            }
            return eventLog;
        }

        public List<Log> ReadLogsOlderThan30()
        {
            _connectionString = GetConnectionString();
            List<Log> logs30DayOlder = new List<Log>();
            Log tempEventLog = new Log();
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
            catch (Exception ex)
            {
                return null;
            }

            return logs30DayOlder;
        }

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
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

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
            catch (Exception ex)
            {
                throw new NullReferenceException();

            }
            return count;

        }

        
    }
}
