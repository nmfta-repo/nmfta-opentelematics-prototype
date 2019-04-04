using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Prototype.OpenTelematics.Api
{
    public class CustomRateLimitConfiguration : RateLimitConfiguration
    {
        protected override void RegisterResolvers()
        {
            base.RegisterResolvers();

            ClientResolvers.Add(new ClientPrincipalResolveContributor(HttpContextAccessor, ClientRateLimitOptions.ClientIdHeader));
        }

        public CustomRateLimitConfiguration(IHttpContextAccessor httpContextAccessor, IOptions<IpRateLimitOptions> ipOptions, IOptions<ClientRateLimitOptions> clientOptions) : base(httpContextAccessor, ipOptions, clientOptions)
        {
        }
    }
}