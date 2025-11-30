using Microsoft.EntityFrameworkCore;

namespace CraiglineApi
{
    public class DevAppDbContext:AppDbContext
    {
        public DevAppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

    }
}
