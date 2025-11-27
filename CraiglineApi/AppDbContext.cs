using Microsoft.EntityFrameworkCore;

namespace CraiglineApi
{
    public class AppDbContext:DbContext
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }
    }
}
