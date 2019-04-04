using System.Linq;
using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Http;

namespace Prototype.OpenTelematics.Api
{
    public class ClientPrincipalResolveContributor : IClientResolveContributor
    {
        private readonly IHttpContextAccessor m_HttpContextAccessor;

        public ClientPrincipalResolveContributor(IHttpContextAccessor httpContextAccessor, string clientIdHeader)
        {
            m_HttpContextAccessor = httpContextAccessor;
        }

        public string ResolveClient()
        {
            // Get Client Id from Claims
            var clientIdClaim = m_HttpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "ClientId");
            if (clientIdClaim != null)
                return clientIdClaim.Value;

            // return default
            return string.Empty;
        }
    }
}