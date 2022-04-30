using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Pentaskilled.MEetAndYou.Entities.DBModels;

namespace Pentaskilled.MEetAndYou.DataAccess.Implementation
{
    public class CopyItineraryDAO
    {
        //private string _connectionString;
        //string _connection = System.Configuration.ConfigurationManager.
        //   ConnectionStrings["MEetAndYouDatabase"].ConnectionString;
        private string _connectionString;
        private MEetAndYouDBContext _dbContext;

        // Constructor
        // Depedencies injectoin for _dbcontext
        public CopyItineraryDAO()
        {
            //var dbOptions = new DbContextOptionsBuilder<MEetAndYouDBContext>().UseSqlServer(_connection).Options;
            //_dbContext = new MEetAndYouDBContext(dbOptions);
            _dbContext = new MEetAndYouDBContext();
        }

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
                        command.Parameters.Add("@eventid", SqlDbType.Int).Value = eventID;
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

        public async Task<List<string>> GetEventCategoryAsync(int eventID)
        {
            _connectionString = GetConnectionString();
            List<string> categoryList = new List<string>();
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    using (SqlCommand command = new SqlCommand("[MEetAndYou].[GetEventCategory](@eventID)", connection))
                    {
                        CommandType commandType = command.CommandType;

                        // Suplly the argument
                        command.Parameters.Add("@eventID", SqlDbType.Int).Value = eventID;
                        Task<SqlDataReader> sqlTaskReader = command.ExecuteReaderAsync();
                        SqlDataReader sqlReader = sqlTaskReader.Result;

                        while (sqlReader.Read())
                        {
                            IDataRecord dataRecord = (IDataRecord)sqlReader;

                            categoryList.Add(dataRecord[1].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return categoryList;
        }

        public async Task<Itinerary> GetItinerary(int intineraryID)
        {
            //var dbcontext = new MEetAndYouDBContext();
            Itinerary itinerary;
            try
            {
                itinerary = await _dbContext.Itineraries.FindAsync(intineraryID);
            }
            catch (SqlException ex)
            {
                //Console.WriteLine("Sql exception occur when getting itinerary");
                //Console.WriteLine(ex.Message);
                //return null;
                throw;
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Exception occur when trying to get itinerary by ID");
                //Console.WriteLine(ex.Message);
                //return null;
                throw;
            }
            return itinerary;
            //throw new NotImplementedException();
        }
    }
}
