using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.Identity.Client;

namespace GamingWebsite.Models {
    public class Game {
        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(250)]
        public string Description { get; set; } = string.Empty;

        [MaxLength(150)]
        public string Cover { get; set; } = string.Empty;

        public int categoryID { get; set; }
        public Category Category { get; set; } = default!;
        
        public ICollection<Platform> Platforms { get; set; } 
    }
}
