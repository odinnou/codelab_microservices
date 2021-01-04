using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Panier.UnitTests.Configuration
{
    public class TestAuthenticationHandler : AuthenticationHandler<TestAuthenticationOptions>
    {
        public TestAuthenticationHandler(IOptionsMonitor<TestAuthenticationOptions> options, ILoggerFactory logger,
            UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var authenticationTicket = new AuthenticationTicket(
                new ClaimsPrincipal(Options.Identity),
                new AuthenticationProperties(),
                "Test Scheme");

            return Task.FromResult(AuthenticateResult.Success(authenticationTicket));
        }
    }

    public static class TestAuthenticationExtensions
    {
        public static AuthenticationBuilder AddTestAuth(this AuthenticationBuilder builder, Action<TestAuthenticationOptions> configureOptions)
        {
            return builder.AddScheme<TestAuthenticationOptions, TestAuthenticationHandler>("Test Scheme", "Test Auth", configureOptions);
        }
    }

    public class TestAuthenticationOptions : AuthenticationSchemeOptions
    {
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        public bool EmailVerified { get; set; }
        public string Uid { get; set; }
        public string UserType { get; set; }
        public virtual ClaimsIdentity Identity
        {
            get
            {
                List<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, Uid),
                    new Claim("email_verified", EmailVerified ? "true" : "false"),
                    new Claim("user_type",  UserType)
                };

                return new ClaimsIdentity(claims.ToArray(), "test");
            }
        }
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
    }
}
