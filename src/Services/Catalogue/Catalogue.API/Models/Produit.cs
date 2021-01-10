using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Catalogue.API.Models
{
    public class Produit
    {
        [Key]
        public int Id { get; set; }
        public string Reference { get; set; }
        public string Libelle { get; set; }

        [JsonIgnore]
        public virtual Famille Famille { get; set; }
    }
}
