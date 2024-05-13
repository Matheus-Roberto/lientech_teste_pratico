using Microsoft.EntityFrameworkCore;
using test_lientech.Data;
using test_lientech.Model;

namespace test_lientech.Tests
{
    public class MovieDbContext : DbContext
    {
        public MovieDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Movie> Movie { get; set; }
    }
}
