using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Prototype.OpenTelematics.Api.Security
{
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