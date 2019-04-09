using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Prototype.OpenTelematics.DataAccess;

namespace Prototype.OpenTelematics.Api.Controllers
{
    /// <summary>
    /// Helper Controller to Create and Add Roles to User
    /// <para>NOTE: ***INTERNAL USE ONLY***. Throw away code and this will not be part of the Telematics API</para>
    /// </summary>
    [ApiController]
    [Authorize]
    public class AccountManagerController : ControllerBase
    {
        private readonly TelematicsContext m_DbContext;
        private readonly UserManager<IdentityUser> m_UserManager;
        private readonly RoleManager<IdentityRole> m_RoleManager;

        public AccountManagerController(TelematicsContext dbContext, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            m_DbContext = dbContext;
            m_UserManager = userManager;
            m_RoleManager = roleManager;
        }

        [Route("api/accountmanager/createuser")]
        [HttpGet]
        public ActionResult<string> CreateUser(string userName, string password, string roles)
        {
            var user = new IdentityUser
            {
                UserName = userName,
                Email = userName + "@test.com"
            };

            var clientId = "CED4868A-B64C-4C40-9D17-A4D58D0A1C5A";
            var result = m_UserManager.CreateAsync(user, password).Result;
            if (result.Succeeded)
            {
                //var createdUser = m_UserManager.FindByNameAsync(user.UserName).Result;
                m_DbContext.AddUserToClient(clientId, user.Id);

                // check role
                if (!string.IsNullOrWhiteSpace(roles))
                {
                    var roleList = roles.Split(",");
                    foreach (var role in roleList)
                    {
                        // add to role
                        var roleResult = m_UserManager.AddToRoleAsync(user, role).Result;
                    }

                }
                return userName;
            }

            return "Invalid";
        }

        [Route("api/accountmanager/addtorole")]
        [HttpGet]
        public ActionResult<string> AddToRole(string userName, string roles)
        {
            var user = m_UserManager.FindByNameAsync(userName).Result;
            // check role
            if (!string.IsNullOrWhiteSpace(roles))
            {
                var roleList = roles.Split(",");
                foreach (var role in roleList)
                {
                    if (string.IsNullOrWhiteSpace(role)) continue;
                    // add to role
                    var roleResult = m_UserManager.AddToRoleAsync(user, role.Trim()).Result;
                }

            }

            return userName;
        }


        [Route("api/accountmanager/createrole")]
        [HttpGet]
        public ActionResult<string> CreateRole()
        {
            var roles = "Vehicle Query,Vehicle Follow, Driver Query,Driver Follow,Driver Dispatch,Driver Duty,HR,Admin".Split(",");
            foreach (var roleName in roles)
            {
                var role = new IdentityRole
                {
                    Name = roleName
                };
                var result = m_RoleManager.CreateAsync(role).Result;
            }

            return "Success";
        }

    }
}