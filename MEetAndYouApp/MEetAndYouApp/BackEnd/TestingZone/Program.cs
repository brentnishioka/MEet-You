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
        AuthnService authnService = new AuthnService();

        // Calling the method to save token to the databse, Brent ID
        string token = authnService.generateToken();
        int userID = 3; 

        bool result = authnDAO.SaveToken(userID, token).Result;
        Console.WriteLine(result);
    }
}