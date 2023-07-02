using BagApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace BagApp.Data
{
    public class BagAppDbContext : DbContext
    {
        public BagAppDbContext(DbContextOptions<BagAppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Bag> Bags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bag>().ToTable("Bags"); 

            base.OnModelCreating(modelBuilder);
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(message => File.AppendAllText("LogsHistory.txt", message));
            base.OnConfiguring(optionsBuilder);
        }

    }
}
