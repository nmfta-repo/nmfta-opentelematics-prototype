using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Prototype.OpenTelematics.DataAccess;

namespace Prototype.OpenTelematics.Api.Security
{
    public interface IBasicAuthenticationService
    {
        TelematicsContext DbContext { get;}
        bool IsValidUser(string user, string password, out IdentityUser identityUser);
        IList<string> GetRolesForUser(IdentityUser user);
    }
}