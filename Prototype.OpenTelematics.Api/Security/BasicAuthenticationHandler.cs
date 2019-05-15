using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Prototype.OpenTelematics.Api.Security
{
    /// <summary>
    /// Core Authentication Handler
    /// </summary>
    public class BasicAuthenticationHandler : AuthenticationHandler<BasicAuthenticationOptions>
    {
        private const string AuthorizationHeaderName = "Authorization";
        private const string BasicSchemeName = "Basic";
        private readonly IBasicAuthenticationService _authenticationService;

        public BasicAuthenticationHandler(
            IOptionsMonitor<BasicAuthenticationOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IBasicAuthenticationService authenticationService)
            : base(options, logger, encoder, clock)
        {
            _authenticationService = authenticationService;
        }

        /// <summary>
        /// Handle the Authentication Processing
        /// <para>Extracts the authentication information from the Header. Validates to make sure the value is in proper format</para>
        /// <para>If Parameters are valid, we hit the back-end to validate the user name and password</para>
        /// <para>If User Name, Password is valid, we extract the applicable Roles for the User</para>
        /// <para>Creates the Claims Principal and attaches the User Claims (User Id, Client Id, Roles) to the current Request</para>
        /// <para>This information is now available in all of the downstream methods of the Request Pipeline</para>
        /// <para>TODO:K How/When to block request if User Name, Password is valid. Support IP white listing?
        /// Differentiate between legitimate access and brute force attacks </para>
        /// </summary>
        /// <returns></returns>
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey(AuthorizationHeaderName))
            {
                //Authorization header not in request
                return AuthenticateResult.NoResult();
            }

            if (!AuthenticationHeaderValue.TryParse(Request.Headers[AuthorizationHeaderName], out var headerValue))
            {
                //Invalid Authorization header
                return AuthenticateResult.NoResult();
            }

            if (!BasicSchemeName.Equals(headerValue.Scheme, StringComparison.OrdinalIgnoreCase))
            {
                //Not Basic authentication header
                return AuthenticateResult.NoResult();
            }

            if (string.IsNullOrWhiteSpace(headerValue.Parameter))
            {
                // Required Parameter is Empty
                return AuthenticateResult.NoResult();
            }

            // Make sure the Authorization header is in proper format
            string userAndPassword;
            try
            {
                var headerValueBytes = Convert.FromBase64String(headerValue.Parameter);
                userAndPassword = Encoding.UTF8.GetString(headerValueBytes);
            }
            catch (Exception)
            {
                return AuthenticateResult.Fail("Invalid Basic authentication header");
            }

            var parts = userAndPassword.Split(':');
            if (parts.Length != 2)
            {
                return AuthenticateResult.Fail("Invalid Basic authentication header");
            }

            // extract the user name and password
            var user = parts[0];
            var password = parts[1];

            // check user name and password
            IdentityUser identityUser;
            var isValidUser = _authenticationService.IsValidUser(user, password, out identityUser);
            if (!isValidUser)
            {
                return AuthenticateResult.Fail("Invalid username or password");
            }

            var client = _authenticationService.DbContext.GetClient(identityUser.Id);
            if (client == null)
            {
                return AuthenticateResult.Fail("Invalid username or password");
            }

            var roles = _authenticationService.GetRolesForUser(identityUser);

            // setup the claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user),
                new Claim(ClaimTypes.NameIdentifier, identityUser.Id),
                new Claim(ClaimTypes.Email, identityUser.Email),
                new Claim("ClientId", client.ClientId),
            };

            // add the role list
            claims.AddRange(roles.Select(c => new Claim(ClaimTypes.Role, c)));

            var identity = new ClaimsIdentity(claims, Scheme.Name);            

            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);
            return AuthenticateResult.Success(ticket);
        }

        /// <summary>
        /// Validation Failed, return proper status. This method also validates to make sure the Request is using HTTPS
        /// </summary>
        /// <param name="properties">Runtime Properties Parameter</param>
        /// <returns>Validation Result</returns>
        protected override async Task HandleChallengeAsync(AuthenticationProperties properties)
        {
            Response.Headers["WWW-Authenticate"] = $"Basic realm=\"{Options.Realm}\", charset=\"UTF-8\"";
            await base.HandleChallengeAsync(properties);
        }
    }
}