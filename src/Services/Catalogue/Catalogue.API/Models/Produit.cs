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
        public bool IsAvailable { get; set; }

        [JsonIgnore] // Habituellement je ferai un DTO contenant exclusivement Id/Reference/Libelle, mais ce n'est pas le sujet de la démo
        public virtual Famille Famille { get; set; }
    }
}
