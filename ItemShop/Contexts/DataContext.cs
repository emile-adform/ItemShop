using ItemShop.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ItemShop.Contexts
{
    public class DataContext : DbContext
    {
        //protected readonly IConfiguration Configuration;
        //public DataContext(IConfiguration configuration)
        //{
        //    Configuration = configuration;
        //}

        //protected override void OnConfiguring(DbContextOptionsBuilder options)
        //{
        //    // connect to postgres with connection string from app settings
        //    options.UseNpgsql(Configuration.GetConnectionString("PostgreConnection"));
        //}
        public DbSet<Item> Items { get; set; }
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
    }
}
