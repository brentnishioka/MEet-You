using Pentaskilled.MEetAndYou.DataAccess;
using Pentaskilled.MEetAndYou.Services.Implementation;

namespace Pentaskilled.MEetAndYou.TestingZone;
public class Program
{
    public static void Main()
    {
        Console.WriteLine("THis is the current datetime: ");
        Console.WriteLine(DateTime.UtcNow.ToString());

        AuthnDAO authnDAO = new AuthnDAO();
        AuthorizationDAO authzDAO = new AuthorizationDAO(); 
        AuthnService authnService = new AuthnService();

        // Calling the method to save token to the databse, Brent ID
        string token = "blueberrystrawberryy";
        //int userID = 4; 

        //bool result = authnDAO.SaveToken(userID, token).Result;
        //Console.WriteLine(result);

        Console.WriteLine("");
        Console.WriteLine("Verifying token: ");
        bool isVerified = authzDAO.VerifyToken(4, token);
        Console.WriteLine("Result: " + isVerified);
    }
}