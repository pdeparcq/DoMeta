using DoMeta.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace DoMeta.Infrastructure
{
    public class MetaDbContext : DbContext
    {
        public MetaDbContext(DbContextOptions<MetaDbContext> options) : base(options)
        {
        }

        public DbSet<EntityData> Entities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EntityData>();
        }
    }
}
