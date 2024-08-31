using GamingWebsite.Models;
using Microsoft.EntityFrameworkCore;

namespace GamingWebsite.Data {
    public class AppContext: DbContext  {
        public AppContext() { }
        public DbSet<Game> Games { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Platform> Platforms { get; set; }
        public AppContext(DbContextOptions<AppContext> options) : base(options) {
             
        
        }
        
    }
}
