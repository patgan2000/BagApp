using BagApp.Components.Models;
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

        public DbSet<BagApp.Entities.Bag> Bags { get; set; }
        public DbSet<BagApp.Components.Models.Bag> BagModels { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(message => File.AppendAllText("LogsHistory.txt", message));
            base.OnConfiguring(optionsBuilder);
        }

    }
}
