using Microsoft.EntityFrameworkCore;

namespace CovadisAPI.Data
{
    public class ApplicationDbContext : DbContext
    {

        //De tables
        public DbSet<ServicesDataModel> Services { get; set; }
        public DbSet<WebsiteModel> Websites { get; set; }
        public DbSet<ElementModel> Elements { get; set; }
        public DbSet<ApiModel> Apis { get; set; }

        //Configurations
        public DbSet<ConfigurationModel> GlobalConfiguration { get; set; }

        //Logs
        public DbSet<WebsiteLog> WebsiteLog { get; set; }
        public DbSet<ElementLog> ElementLog { get; set; }

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
