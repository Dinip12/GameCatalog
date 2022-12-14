using GameCatalog.Entity;
using System.Data.Entity;
using System.Collections.Generic;

namespace GameCatalog.Data
{
    public class AppDbContext : DbContext
    {        
        public DbSet<Game> Games { get; set; }
        public AppDbContext() : base("Server=(localdb)\\mssqllocaldb;Database=GameCatalog;Trusted_Connection=True;")
        {
            Games = this.Set<Game>();            
        }
    }
}
