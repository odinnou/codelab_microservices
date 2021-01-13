using System;
using System.Runtime.Serialization;

namespace Catalogue.API.Infrastructure.Exceptions
{
    [Serializable]
    public class ProduitNotFoundException : Exception
    {
        public ProduitNotFoundException(string reference) : base($"No produit found for reference : {reference}")
        {
        }

        protected ProduitNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
