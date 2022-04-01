using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;
using System.Security.Permissions;
using System.Threading;
using Pentaskilled.MEetAndYou.Services.Implementation;
using Pentaskilled.MEetAndYou.DataAccess;
using Pentaskilled.MEetAndYou.DataAccess.Contracts;
using PentaSkilled.MEetAndYou.Authorization.Contracts;
using PentaSkilled.MEetAndYou.Authorization.Model;
using System.Data.SqlClient;
using PentaSkilled.MEetAndYou.Authorization.Implementation;

namespace Pentaskilled.MEetAndYou.Managers
{
    public class AuthorizationManager
    {
        private  IAuthorizeService _authzService;
        private  IAuthorizationDAO _authzDAO;

        public AuthorizationManager()
        {
            _authzService = new AuthorizationService();
            _authzDAO = new AuthorizationDAO();
        }

        public bool IsAuthorized(int userID, string userToken, string role)
        {

            try
            {
                // Verify to see if the user already signed in 
                // By Verify to see if the user token exist in the UserToken table
                bool isVerified = _authzDAO.VerifyToken(userID, userToken);

                //Create User Identity object using the userID
                // Get the roles of the user from the database using userID
                UserIdentity userIdentity = new UserIdentity(userID.ToString());
                List<string> roleList = new List<string>();
                if (isVerified)
                {
                    roleList = _authzDAO.GetRoles(userID);
                }

                // Create the User Principle object
                UserPrincipal userPrincipal = new UserPrincipal(userIdentity, roleList);

                bool isAuthorized = _authzService.IsAuthorized(userPrincipal, role);

                return isAuthorized;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Database error when using Authorization Manager");
                throw;
                //return false;
            }
            catch (Exception ex)
            {
                throw;
                //return false;
            }
        }
    }
}
