﻿using Pentaskilled.MEetAndYou.Entities;
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
            return @"Data Source=DESKTOP-DBE5DM2;Initial Catalog=MEetAndYou-DB;Integrated Security=True";
        }

        /// <summary>
        /// Inserts system logs into the respective table "SystemEventLogs" in the database.
        /// </summary>
        /// <param name="sysLog"></param>
        /// <returns>  
        ///     True -> the log is inserted into the database successfully.
        ///     False -> the log is not successfully inserted into the database.</returns>
        public bool PushLogToDB(SystemLog sysLog)
        {
            _connectionString = GetConnectionString();
            Enum logLvl = sysLog.logLevel;
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                using (SqlCommand command = new SqlCommand("[MEetAndYou].[InsertSysLog]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@dateTime", SqlDbType.DateTime).Value = sysLog.dateTime;
                    command.Parameters.Add("@category", SqlDbType.VarChar).Value = sysLog.category;
                    command.Parameters.Add("@logLevel", SqlDbType.VarChar).Value = logLvl.ToString();
                    command.Parameters.Add("@message", SqlDbType.VarChar).Value = sysLog.message;

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

        /// <summary>
        /// Inserts user logs into the respective table "UserEventLogs" in the database.
        /// </summary>
        /// <param name="userLog"> The data transfer object (DTO) of all our user log information. </param>
        /// <returns>
        ///     True -> the log is inserted into the database successfully.
        ///     False -> the log is not successfully inserted into the database.
        /// </returns>
        public bool PushLogToDB(UserLog userLog)
        {
            _connectionString = GetConnectionString();
            Enum logLvl = userLog.logLevel;
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                using (SqlCommand command = new SqlCommand("[MEetAndYou].[InsertUserLog]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@dateTime", SqlDbType.DateTime).Value = userLog.dateTime;
                    command.Parameters.Add("@category", SqlDbType.VarChar).Value = userLog.category;
                    command.Parameters.Add("@logLevel", SqlDbType.VarChar).Value = logLvl.ToString();
                    command.Parameters.Add("@userId", SqlDbType.Int).Value = userLog.userId;
                    command.Parameters.Add("@message", SqlDbType.VarChar).Value = userLog.message;

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

        public int GetCurrentSysIdentity()
        {
            int lastLogId = 0;
            _connectionString = GetConnectionString();
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    SqlCommand command = new SqlCommand("SELECT [MEetAndYou].[GetCurrentSysIdentity]()", connection);
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

        public int GetCurrentUserIdentity()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Checks to see if a system log exists in our database.
        /// </summary>
        /// <param name="sysLog"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public int CheckExistingLog(SystemLog sysLog)
        {
            int numRows = 0;
            _connectionString = GetConnectionString();
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    SqlCommand command = new SqlCommand("SELECT [MEetAndYou].[CheckExistingSysLog]()", connection);
                    command.Parameters.Add(new SqlParameter("@sysLogId", SqlDbType.Int));
                    command.Parameters["@sysLogId"].Value = sysLog.logId;
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

        public int CheckExistingLog(UserLog userLog)
        {
            throw new NotImplementedException();
        }

/*        public List<Log> ReadLogsOlderThan30()
        {
            _connectionString = GetConnectionString();
            List<Log> logs30DayOlder;
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                using (SqlCommand command = new SqlCommand("[MEetAndYou].[sysLogs30DaysOld]", connection))
                {

                }
            }
            catch (Exception ex)
            {
                return null;
            }

            return logs30DayOlder;
        }*/
    }
}
