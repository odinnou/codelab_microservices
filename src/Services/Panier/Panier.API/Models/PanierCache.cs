using System.Collections.Generic;

namespace Panier.API.Models
{
    /// <summary>
    /// Ne doit JAMAIS être fait, c'est vraiment pour simuler une resource non partagée
    /// Je n'ai pas implémenté le système de TTL (donné éphémère)
    /// </summary>
    public class PanierCache
    {
        public PanierCache()
        {
            Content = new Dictionary<string, List<PanierItem>>();
        }

        public Dictionary<string, List<PanierItem>> Content { get; }
    }
}
