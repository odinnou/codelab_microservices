using System.ComponentModel.DataAnnotations;

namespace Catalogue.API.Models
{
    public class Famille
    {
        [Key]
        public int Id { get; set; }
        public string Nom { get; set; }
    }
}
