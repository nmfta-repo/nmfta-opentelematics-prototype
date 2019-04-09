using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Prototype.OpenTelematics.Api.Security
{
    /// <summary>
    /// DB Context that provides access to the back-end store that has the User Information
    /// </summary>
    public partial class SecurityDbContext :
        IdentityDbContext<IdentityUser>
    {
        public SecurityDbContext(DbContextOptions<SecurityDbContext>
            options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}