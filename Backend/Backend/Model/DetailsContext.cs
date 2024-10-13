using Microsoft.EntityFrameworkCore;

namespace Backend.Model
{
    public class DetailsContext : DbContext
    {
        public DetailsContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Details> Details { get; set; }
    }
}
