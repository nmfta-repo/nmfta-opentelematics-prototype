using Microsoft.AspNetCore.Authentication;

namespace Prototype.OpenTelematics.Api.Security
{
    public class BasicAuthenticationOptions : AuthenticationSchemeOptions
    {
        public string Realm { get; set; }
    }
}