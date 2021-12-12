using Xunit;
using Pentaskilled.MEetAndYou.DataAccess;
using Pentaskilled.MEetAndYou.Entities;
using System;
using System.Data.SqlClient;
using System.Data;

namespace Pentaskilled.MEetAndYou.XUnitTests
{
    public class UMTests
    {
        [Fact]
        public void CreateAccountRecordTest()
        {
            UserAccountEntity ua = new UserAccountEntity();

            ua.Email = "viviand2465@gmail.com";
            ua.Password = "joshishandsome1";
            ua.PhoneNumber = "4084802185";
            ua.Role = "Regular User";
            ua.RegisterDate = DateTime.UtcNow;
            ua.Active = 1;

            IUMDAO _UMDAO = new UMDAO();
            bool isSuccessfullyCreated = true;

            Assert.Equal(isSuccessfullyCreated, _UMDAO.CreateAccountRecord(ua));
        }

        [Fact]
        public void GetAccountRecordTest()
        {         
            string _connectionString = @"Data Source=JDCRAMOS;Initial Catalog=MEetAndYouDB;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = new SqlCommand("[MEetAndYou].[GetAccountRecord]", connection))
            {
                command.CommandType = CommandType.Text;
                command.Parameters.Add("@id", SqlDbType.Int).Value = 2;

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }

            Assert.Equal(ua.Email, "John.Doe@gmail.com");
        }
    }
}
