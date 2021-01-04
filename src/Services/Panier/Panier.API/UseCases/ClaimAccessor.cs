using Microsoft.AspNetCore.Http;
using Panier.API.Infrastructure.Exceptions;
using System.Linq;
using System.Security.Claims;

namespace Panier.API.UseCases
{
    public class ClaimAccessor : IClaimAccessor
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public ClaimAccessor(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public string GetUidFromClaims()
        {
            Claim? uidClaim = httpContextAccessor.HttpContext?.User?.Claims?.SingleOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier);

            if (uidClaim == null)
            {
                throw new ClaimNotFoundException(ClaimTypes.NameIdentifier);
            }

            return uidClaim.Value;
        }
    }
}
