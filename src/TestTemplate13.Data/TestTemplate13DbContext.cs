using MassTransit;
using Microsoft.EntityFrameworkCore;
using TestTemplate13.Core.Entities;

namespace TestTemplate13.Data
{
    public class TestTemplate13DbContext : DbContext
    {
        public TestTemplate13DbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Foo> Foos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.AddInboxStateEntity();
            modelBuilder.AddOutboxMessageEntity();
            modelBuilder.AddOutboxStateEntity();
        }
    }
}
