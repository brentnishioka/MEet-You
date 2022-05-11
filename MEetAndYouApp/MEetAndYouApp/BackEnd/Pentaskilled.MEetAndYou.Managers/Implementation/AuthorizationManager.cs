using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Pentaskilled.MEetAndYou.DataAccess;
using Pentaskilled.MEetAndYou.DataAccess.Contracts;
using PentaSkilled.MEetAndYou.Authorization.Contracts;
using PentaSkilled.MEetAndYou.Authorization.Implementation;
using PentaSkilled.MEetAndYou.Authorization.Model;

namespace Pentaskilled.MEetAndYou.Managers
{
    public class AuthorizationManager : IAuthorizationManager
    {
        private IAuthorizeService _authzService;
        private IAuthorizationDAO _authzDAO;

        public AuthorizationManager()
        {
            _authzService = new AuthorizationService();
            _authzDAO = new AuthorizationDAO();
        }

        /// <summary>
        /// Verifies an user and their claimed roles to see if matched with the "UserToken" and "UserRoles".
        /// </summary>
        /// <param name="userID"> the userID from "UserAccountRecords"</param>
        /// <param name="role"> the role that the user claim the have</param>
        /// <param name="userToken">the token that to verify user identity</param>
        /// <returns>  
        ///     True -> If the user have the the role they claimed they do, 
        ///     False -> If the token is does not exist, or wrong userID or the user does not have the claimed role 
        /// </returns>
        public bool IsAuthorized(int userID, string userToken, string role)
        {
            try
            {
                // Verify to see if the user already signed in 
                // By Verify to see if the user token exist in the UserToken table
                bool isAuthorized = false;
                bool isVerified = _authzDAO.VerifyToken(userID, userToken);

                //Create User Identity object using the userID
                // Get the roles of the user from the database using userID if the user is verified
                UserIdentity userIdentity = new UserIdentity(userID.ToString());
                List<string> roleList = new List<string>();
                if (isVerified)
                {
                    roleList = _authzDAO.GetRoles(userID);
                }
                else
                {
                    return isAuthorized;
                }

                // Create the User Principle object
                UserPrincipal userPrincipal = new UserPrincipal(userIdentity, roleList);

                // Check to see if the user have the roles that they claimed
                isAuthorized = _authzService.IsAuthorized(userPrincipal, role);

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
