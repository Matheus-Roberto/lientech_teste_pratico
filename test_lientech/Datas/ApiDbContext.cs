using Microsoft.EntityFrameworkCore;
using test_lientech.Model;

namespace test_lientech.Data
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options) { }
        public DbSet<Movie> Movie { get; set; }
        public DbSet<Room> Room { get; set; }

    }
}
