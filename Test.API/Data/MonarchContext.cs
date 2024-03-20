using Microsoft.EntityFrameworkCore;
using Vienna.Test.API.Entites;

namespace Vienna.Test.API.Data
{
    public class MonarchContext : DbContext
    {
        public MonarchContext(DbContextOptions<MonarchContext> options) : base(options)
        {
        }

        public DbSet<Monarch> Monarches { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Monarch>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever(); 
                                                                  
            });

        }

    }
}
