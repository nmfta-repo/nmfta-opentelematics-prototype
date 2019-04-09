using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Prototype.OpenTelematics.DataAccess;

namespace Prototype.OpenTelematics.Api.Security
{
    /// <summary>
    /// Basic Authentication Service Interface
    /// </summary>
    public interface IBasicAuthenticationService
    {
        TelematicsContext DbContext { get;}
        bool IsValidUser(string user, string password, out IdentityUser identityUser);
        IList<string> GetRolesForUser(IdentityUser user);
    }
}