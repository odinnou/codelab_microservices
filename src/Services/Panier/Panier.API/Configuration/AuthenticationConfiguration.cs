using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Panier.API.Infrastructure;
using Panier.API.Infrastructure.Filters;
using System.Collections.Generic;

namespace Panier.API.Configuration
{
    public static class AuthenticationConfig
    {
        public static IServiceCollection AddAuthentications(this IServiceCollection services, AppSettings appSettings)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(jwtOptions =>
            {
                jwtOptions.Authority = appSettings.AuthorityConfiguration.Authority;
                jwtOptions.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = appSettings.AuthorityConfiguration.Authority,
                    ValidateAudience = true,
                    ValidAudience = appSettings.AuthorityConfiguration.Audience,
                    ValidateLifetime = true
                };
            });

            services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder().RequireClaim("email_verified", new List<string> { "true" }).Build();
                options.AddPolicy(CommonController.ONLY_PANIER_ADMIN_POLICY, policy => policy.RequireClaim("user_type", new List<string> { "admin" }).AddRequirements(new AdminCheckerRequirement()));
                options.AddPolicy(CommonController.ONLY_CLIENT_POLICY, policy => policy.RequireClaim("user_type", new List<string> { "client" }));
            });

            services.AddTransient<IAuthorizationHandler, AdminCheckerRequirementHandler>();

            return services;
        }
    }
}
