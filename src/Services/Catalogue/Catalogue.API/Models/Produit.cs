using System.ComponentModel.DataAnnotations;

namespace Catalogue.API.Models
{
    public class Produit
    {
        [Key]
        public int Id { get; set; }
        public string Reference { get; set; }
        public string Libelle { get; set; }
        public virtual Famille Famille { get; set; }
    }
}
