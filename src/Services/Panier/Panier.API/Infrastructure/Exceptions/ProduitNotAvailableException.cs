using System;
using System.Runtime.Serialization;

namespace Panier.API.Infrastructure.Exceptions
{
    [Serializable]
    public class ProduitNotAvailableException : Exception
    {
        public ProduitNotAvailableException(string reference) : base($"Produit not available for reference : {reference}")
        {
        }

        protected ProduitNotAvailableException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
