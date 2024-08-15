using Microsoft.EntityFrameworkCore;
using ShopApp.Entities;
using System.Reflection;


namespace ShopApp.Data
{
    public class ShopAppContext:DbContext
    {
        public ShopAppContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
         
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}

