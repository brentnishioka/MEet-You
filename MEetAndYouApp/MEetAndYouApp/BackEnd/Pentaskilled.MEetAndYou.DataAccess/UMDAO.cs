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
            return @"Data Source=JDCRAMOS;Initial Catalog=MEetAndYouDB;Integrated Security=True";
        }

        /// <summary>
        /// Inserts system logs into the respective table "SystemEventLogs" in the database.
        /// </summary>
        /// <param name="ua"></param>
        /// <returns>  
        ///     True -> the UserAccount is inserted into the database successfully.
        ///     False -> the UserAccount is not successfully inserted into the database.
        /// </returns>
        public bool CreateAccountRecord(UserAccountEntity ua)
        {
            _connectionString = GetConnectionString();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                using (SqlCommand command = new SqlCommand("[MEetAndYou].[CreateAccountRecord]", connection))
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

        public bool UpdateAccountEmail(UserAccountEntity ua)
        {

        }

        public bool UpdateAccountPassword(UserAccountEntity ua)
        {

        }

        public bool UpdateAccountPhone(UserAccountEntity ua)
        {

        }

        public bool UpdateAccountRole(UserAccountEntity ua)
        {

        }

        public bool VerifyUserInDB(UserAccountEntity ua)
        {

        }
    }
}
