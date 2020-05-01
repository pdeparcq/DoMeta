using DoMeta.Infrastructure.CodeGen.Entities;
using Microsoft.EntityFrameworkCore;

namespace DoMeta.Infrastructure.CodeGen
{
    public class CodeGenDbContext : DbContext
    {
        public CodeGenDbContext(DbContextOptions<CodeGenDbContext> options) : base(options)
        {
        }

        public DbSet<CodeTemplate> CodeTemplates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CodeTemplate>()
                .Property(t => t.Name).IsRequired();
        }
    }
}
