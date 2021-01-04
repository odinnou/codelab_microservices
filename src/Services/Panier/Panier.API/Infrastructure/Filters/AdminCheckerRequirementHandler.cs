using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Panier.API.UseCases;
using System;
using System.Threading.Tasks;

namespace Panier.API.Infrastructure.Filters
{
    public class AdminCheckerRequirement : IAuthorizationRequirement
    {
    }

    public class AdminCheckerRequirementHandler : AuthorizationHandler<AdminCheckerRequirement>
    {
        private readonly ILogger<AdminCheckerRequirementHandler> iLogger;
        private readonly IClaimAccessor iClaimAccessor;

        public AdminCheckerRequirementHandler(IClaimAccessor iClaimAccessor, ILogger<AdminCheckerRequirementHandler> iLogger)
        {
            this.iClaimAccessor = iClaimAccessor;
            this.iLogger = iLogger;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, AdminCheckerRequirement requirement)
        {
            // Ce await n'est pas nécessaire lorsque un await est utilisé au sein de la méthode (accès DB par exemple)
            await Task.Run(() =>
            {
                try
                {
                    string onlyPanierAdminUid = "U2vCQ94KJTYBeHSUisSKLgM4mdW2";
                    string uid = iClaimAccessor.GetUidFromClaims();

                    // Ce test est juste un exemple de cas "complexe", habituellement ça pourrait plutôt être un accès DB ou une comparaison avec un autre header par exemple
                    if (uid == onlyPanierAdminUid)
                    {
                        context.Succeed(requirement);
                    }
                    else
                    {
                        iLogger.LogWarning($"Admin uid : '{uid}' is not a panier admin");
                    }
                }
                catch (Exception exc)
                {
                    iLogger.LogError(exc, "Not handled exception thrown");
                }
            });
        }
    }
}
