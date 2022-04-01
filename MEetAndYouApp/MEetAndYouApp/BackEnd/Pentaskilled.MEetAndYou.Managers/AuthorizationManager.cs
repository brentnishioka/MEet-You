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

        //public bool IsAuthorized(string userToken, string role) {

        //    try
        //    {
        //        int userID = _authzDAO.VerifyToken(userToken).Result;

        //        //Create User Identity object
        //        UserIdentity userIdentity = new UserIdentity(userID.ToString());
        //        List<string> roleList = _authzDAO.GetRoles(userToken).Result;

        //        // Create the User Principle object
        //        UserPrincipal userPrincipal = new UserPrincipal(userIdentity, roleList);

        //        bool isAuthorized = _authzService.IsAuthorized(userPrincipal, role);

        //        return isAuthorized;
        //    }
        //    catch (SqlException ex)
        //    {
        //        return false;
        //    }
        //    catch(Exception ex)
        //    {
        //        return false;
        //    }
        //}
    }
}
