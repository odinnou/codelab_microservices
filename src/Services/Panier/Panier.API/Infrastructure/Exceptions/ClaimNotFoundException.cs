using System;
using System.Runtime.Serialization;

namespace Panier.API.Infrastructure.Exceptions
{
    [Serializable]
    public class ClaimNotFoundException : Exception
    {
        public ClaimNotFoundException(string claimName) : base($"Claim : '{claimName}' not found")
        {
        }

        protected ClaimNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
