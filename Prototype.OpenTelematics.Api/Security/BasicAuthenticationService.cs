using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Prototype.OpenTelematics.DataAccess;

namespace Prototype.OpenTelematics.Api.Security
{
    public class BasicAuthenticationService : IBasicAuthenticationService
    {
        private readonly UserManager<IdentityUser> m_UserManager;
        private readonly RoleManager<IdentityRole> m_RoleManager;
        public TelematicsContext DbContext { get; }

        public BasicAuthenticationService(TelematicsContext dbContext, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            DbContext = dbContext;
            m_UserManager = userManager;
            m_RoleManager = roleManager;
        }

        /// <summary>
        /// Validates User and Password
        /// </summary>
        /// <param name="user">User Name</param>
        /// <param name="password">Password</param>
        /// <param name="identityUser">Out parameter - If User Name and Password is valid, we return the User Information</param>
        /// <returns>True if Valid, False Otherwise</returns>
        public bool IsValidUser(string user, string password, out IdentityUser identityUser)
        {
            identityUser = m_UserManager.FindByNameAsync(user).Result;
            if (identityUser == null)
            {
                return false;
            }

            return m_UserManager.CheckPasswordAsync(identityUser, password).Result;
        }

        /// <summary>
        /// Returns the list of Roles for Passed in User
        /// </summary>
        /// <param name="user">Validated User</param>
        /// <returns>List of roles for the User</returns>
        public IList<string> GetRolesForUser(IdentityUser user)
        {
            var roles = m_UserManager.GetRolesAsync(user).Result;
            return roles;
        }
    }
}