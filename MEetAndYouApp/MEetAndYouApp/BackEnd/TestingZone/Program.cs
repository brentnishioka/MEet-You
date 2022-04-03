using Pentaskilled.MEetAndYou.DataAccess;
using Pentaskilled.MEetAndYou.DataAccess.Implementation;
using Pentaskilled.MEetAndYou.Entities.DBModels;
using Pentaskilled.MEetAndYou.Managers;
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
        AuthorizationManager authzController = new AuthorizationManager();
        CopyItineraryDAO copyItinDAO = new CopyItineraryDAO();

        // Calling the method to save token to the databse, Brent ID
        //string token = "blueberrystrawberryy";
        //string brokenToken = "blu123rrystraw456ryy";
        //int userID = 4;
        //string claimedRole = "Admin";

        //bool result = authnDAO.SaveToken(userID, token).Result;
        //Console.WriteLine(result);

        //Console.WriteLine("");
        //Console.WriteLine("Verifying token: ");
        //bool isVerified = authzDAO.VerifyToken(userID, token);
        //Console.WriteLine("Result: " + isVerified);

        //Removing the token after verfication
        //Console.WriteLine("");
        //Console.WriteLine("Start removing token: ");
        //bool isDeleted = authnDAO.DeleteToken(userID).Result;
        //Console.WriteLine("Result: " + isDeleted);

        // Testing the Authorization Manager
        //Console.WriteLine("");
        //Console.WriteLine("Start verifying claimed role: ");
        //bool actualValue = authzController.IsAuthorized(userID, token, claimedRole);
        //Console.WriteLine("Result: " + actualValue);

        // Testing Itinerary DAO
        Console.WriteLine("Before getting itin");
        int itineraryID = 2;
        Itinerary itinerary = copyItinDAO.GetItinerary(itineraryID).Result;
        if (itinerary != null)
        {
            Console.Write("Resulting itinerary: ");
            Console.WriteLine(itinerary);
        }
    }
}