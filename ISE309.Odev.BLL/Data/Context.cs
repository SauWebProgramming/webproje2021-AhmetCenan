using ISE309.Odev.Core.DbEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISE309.Odev.BLL.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MenuProduct>()
                .HasKey(k => new { k.MenuID, k.ProductID });
            modelBuilder.Entity<MenuProduct>()
                .HasOne(x => x.Menu)
                .WithMany(y => y.MenuProducts)
                .HasForeignKey(z => z.MenuID);
            modelBuilder.Entity<MenuProduct>()
                .HasOne(x => x.Product)
                .WithMany(y => y.MenuProducts)
                .HasForeignKey(z => z.ProductID);
        }

        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Weight> Weights { get; set; }
    }
}
