using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CovadisAPI.Data
{
    public class ApplicationDbContext : DbContext
    {

        //De tables
        public DbSet<ServicesDataModel> Services { get; set; }
        public DbSet<WebsitesDataModel> Websites { get; set; }
        public DbSet<ElementsDataModel> Elements { get; set; }
        //public DbSet<WebsiteElements> WebsiteElements { get; set; }

        public ApplicationDbContext()
        {
            
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(@"Server =.\MSSQLSERVER01; Database = CovadisDMT;Trusted_Connection=True;MultipleActiveResultSets=true ");
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
    }
}
