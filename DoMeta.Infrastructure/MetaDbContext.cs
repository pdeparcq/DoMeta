using DoMeta.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace DoMeta.Infrastructure
{
    public class MetaDbContext : DbContext
    {
        public MetaDbContext(DbContextOptions<MetaDbContext> options) : base(options)
        {
        }

        public DbSet<MetaType> MetaTypes { get; set; }
        public DbSet<Entity> Entities { get; set; }
        public DbSet<ValueObject> ValueObjects { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<EntityRelation> EntityRelations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MetaType>()
                .HasMany(t => t.Properties)
                .WithOne(p => p.Parent)
                .HasForeignKey(p => p.ParentId)
                .IsRequired();

            modelBuilder.Entity<Property>()
                .HasKey(p => new {p.ParentId, p.Name});

            modelBuilder.Entity<Property>()
                .HasOne(p => p.MetaType)
                .WithMany()
                .HasForeignKey(p => p.MetaTypeId);

            modelBuilder.Entity<Entity>()
                .HasOne(e => e.Identity)
                .WithMany().HasForeignKey(e => new { e.MetaTypeId, e.IdentityPropertyName }).IsRequired();

            modelBuilder.Entity<EntityRelation>()
                .HasKey(p => new { p.ParentId, p.Name });

            modelBuilder.Entity<Entity>()
                .HasMany(e => e.Relations)
                .WithOne(r => r.Parent)
                .HasForeignKey(r => r.ParentId)
                .IsRequired();
        }
    }
}
