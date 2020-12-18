using System.ComponentModel.DataAnnotations;

namespace Panier.API.Models
{
    public class PanierItem
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string Product { get; set; }
    }
}
