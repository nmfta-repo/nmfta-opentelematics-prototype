using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Prototype.OpenTelematics.DataAccess;

namespace Prototype.OpenTelematics.Api.Security
{
    public class BasicAuthenticationService : IBasicAuthenticationService
    {
        private readonly TelematicsContext m_DbContext;
        private readonly UserManager<IdentityUser> m_UserManager;
        private readonly RoleManager<IdentityRole> m_RoleManager;

        public BasicAuthenticationService(TelematicsContext dbContext, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            m_DbContext = dbContext;
            m_UserManager = userManager;
            m_RoleManager = roleManager;
        }

        public TelematicsContext DbContext => m_DbContext;

        public bool IsValidUser(string user, string password, out IdentityUser identityUser)
        {
            identityUser = m_UserManager.FindByNameAsync(user).Result;
            if (identityUser == null)
            {
                return false;
            }

            return m_UserManager.CheckPasswordAsync(identityUser, password).Result;
        }

        public IList<string> GetRolesForUser(IdentityUser user)
        {
            var roles = m_UserManager.GetRolesAsync(user).Result;
            return roles;
        }
    }
}