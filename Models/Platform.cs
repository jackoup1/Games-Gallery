using System.ComponentModel.DataAnnotations;

namespace GamingWebsite.Models {
    public class Platform {
        public int Id { get; set; }
        public string Name { get; set; }= string.Empty;
        public string Icon { get; set; } = string.Empty;

        public ICollection<Game> Games { get; set; }
    }
}
