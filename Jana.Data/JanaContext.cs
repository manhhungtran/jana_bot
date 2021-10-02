using Microsoft.EntityFrameworkCore;
using System;

namespace Jana.Data
{
    public class JanaContext : DbContext
    {
        public JanaContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
