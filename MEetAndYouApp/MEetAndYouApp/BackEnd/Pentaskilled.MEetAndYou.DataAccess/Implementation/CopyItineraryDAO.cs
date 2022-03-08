﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pentaskilled.MEetAndYou.Entities;

namespace Pentaskilled.MEetAndYou.DataAccess.Implementation
{
    public class CopyItineraryDAO
    {
        private string _connectionString;

        // GetConnectionString() from https://docs.microsoft.com/en-us/dotnet/api/system.data.sqlclient.sqlconnection.connectionstring?view=dotnet-plat-ext-6.0
        static private string GetConnectionString()
        {
            return new ConnectionString().ToString();
        }

        public async Task<Event> GetEventAsync(int eventID)
        {
            _connectionString = GetConnectionString();
            Event resultEvent;
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    using (SqlCommand command = new SqlCommand("[MEetAndYou].[GetEvent](@eventID)", connection))
                    {
                        command.Parameters.Add("@id", SqlDbType.Int).Value = eventID;
                        Task<SqlDataReader> sqlTaskReader = command.ExecuteReaderAsync();
                        SqlDataReader sqlReader = sqlTaskReader.Result;

                        // Map the data to the value of the object
                        int newID = sqlReader.GetFieldValue<int>(0);
                        string name = sqlReader.GetFieldValue<string>(1);
                        string description = sqlReader.GetFieldValue<string>(2);
                        string address = sqlReader.GetFieldValue<string>(3);
                        float price = sqlReader.GetFieldValue<float>(4);
                        DateTime dateTime = sqlReader.GetFieldValue<DateTime>(5);
                        resultEvent = new Event(newID, name, description, address, price, dateTime);
                    }
                }

            }
            catch (Exception ex)
            {
                return null;
            }

            return resultEvent;
        }
    }
}