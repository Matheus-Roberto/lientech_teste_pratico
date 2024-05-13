using Microsoft.EntityFrameworkCore;
using test_lientech.Data;
using test_lientech.Model;

namespace test_lientech.Tests
{
    public class RoomDbContext : DbContext
    {
        public RoomDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Room> Room { get; set; }
    }
}
