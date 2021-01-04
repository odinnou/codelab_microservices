using System.ComponentModel.DataAnnotations;

namespace Panier.API.Models
{
    public class PanierItem
    {
        [Required]
        public string Product { get; set; }
    }
}
