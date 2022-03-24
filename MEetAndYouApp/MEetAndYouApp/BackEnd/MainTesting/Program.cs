// See https://aka.ms/new-console-template for more information
using System;
using System.Data.SqlClient;
using Pentaskilled.MEetAndYou.DataAccess;

namespace Pentaskilled.MEetAndYou.MainTesting
{
    public class Program
    {
        public static void Main()
        {
            AuthorizationDAO AuthzDAO = new AuthorizationDAO();
            AuthzDAO.ConnectionString = new ConnectionString().ToString();
            bool accountExists = true;
            Console.WriteLine("result " + AuthzDAO.ConnectionString);
            //List<string> resultAll = AuthzDAO.GetAllRoles().Result;
            List<string> result = AuthzDAO.GetRoles(2);
            Console.WriteLine("Resulting roles: ");
            Console.WriteLine("count: " + result.Count);
            foreach (string r in result)
            {
                Console.WriteLine(r);
            }
        }
    }
}

