using Microsoft.AspNetCore.Authentication;

namespace Prototype.OpenTelematics.Api.Security
{
    /// <summary>
    /// Basic Authentication Realm
    /// </summary>
    public class BasicAuthenticationOptions : AuthenticationSchemeOptions
    {
        public string Realm { get; set; }
    }
}