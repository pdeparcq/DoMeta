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
        public DbSet<ValueObject> ValueObjects { get; set; }
        public DbSet<Entity> Entities { get; set; }
        public DbSet<DomainEvent> DomainEvents { get; set; }
        public DbSet<DomainEventProperty> DomainEventProperties { get; set; }
        public DbSet<EntityProperty> EntityProperties { get; set; }
        public DbSet<EntityRelation> EntityRelations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MetaType>()
                .HasMany(t => t.Properties)
                .WithOne(p => p.Parent)
                .HasForeignKey(p => p.ParentId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();

            modelBuilder.Entity<Entity>()
                .HasMany(e => e.DomainEvents)
                .WithOne(p => p.Parent)
                .HasForeignKey(p => p.ParentId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();

            modelBuilder.Entity<DomainEvent>()
                .HasKey(de => new {de.ParentId, de.Name});

            modelBuilder.Entity<DomainEvent>()
                .HasMany(de => de.Properties)
                .WithOne(p => p.DomainEvent)
                .HasForeignKey(p => new { p.DomainEventEntityId, p.DomainEventName })
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();

            modelBuilder.Entity<DomainEventProperty>()
                .HasKey(p => new { p.DomainEventEntityId, p.DomainEventName, p.Name });

            modelBuilder.Entity<DomainEventProperty>()
                .HasOne(p => p.MetaType)
                .WithMany()
                .HasForeignKey(p => p.MetaTypeId);

            modelBuilder.Entity<EntityProperty>()
                .HasKey(p => new {p.ParentId, p.Name});

            modelBuilder.Entity<EntityProperty>()
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
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();
        }
    }
}
