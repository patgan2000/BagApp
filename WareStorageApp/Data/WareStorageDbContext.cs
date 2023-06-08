using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WareStorageApp.Entities;

namespace WareStorageApp.Data
{
    public class WareStorageDbContext : DbContext
    {
        public DbSet<Ware> Wares => Set<Ware>();

        public DbSet<Other> Others => Set<Other>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseInMemoryDatabase(databaseName: "StorageAppDb");

        }

    }
}
