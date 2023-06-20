using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WareStorageApp.Entities;

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
