using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using test_lientech.Data;
using test_lientech.Tests;

namespace lientech_xunit.Helpers
{
    internal class MockDb : IDbContextFactory<MovieDbContext>
    {
        public MovieDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<MovieDbContext>()
                .UseInMemoryDatabase($"InMEmoryTestDb-{DateTime.Now.ToFileTimeUtc()}")
                .Options;
            return new MovieDbContext( options );
        }
        public RoomDbContext RoomCreateDbContext()
        {
            var options = new DbContextOptionsBuilder<RoomDbContext>()
                .UseInMemoryDatabase($"InMEmoryTestDb-{DateTime.Now.ToFileTimeUtc()}")
                .Options;
            return new RoomDbContext(options);
        }
    }
}
