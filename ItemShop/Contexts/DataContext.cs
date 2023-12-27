using ItemShop.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ItemShop.Contexts
{
    public class DataContext : DbContext
    {
        public DbSet<Item> Items { get; set; }
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
    }
}
