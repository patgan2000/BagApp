using BagApp.Components.CsvReader.Models;
using Microsoft.EntityFrameworkCore;

namespace BagApp.Data
{
    public class BagDbContext : DbContext
    {
        public DbSet<Bag> Bags => Set<Bag>();


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseInMemoryDatabase(databaseName: "StorageAppDb");

        }

    }
}
