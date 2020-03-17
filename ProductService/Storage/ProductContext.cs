using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ProductService.Entities;

namespace ProductService.Storage {
  public class ProductContext : DbContext {
    public ProductContext (DbContextOptions<ProductContext> options) : base (options) { }
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    protected override void OnModelCreating (ModelBuilder modelBuilder) {
      var categories = new List<Category> () {
        new Category {
        Id = 1,
        Name = "Electronics",
        Description = "Electronic Items",
        },
        new Category {
        Id = 2,
        Name = "Clothes",
        Description = "Dresses",
        },
        new Category {
        Id = 3,
        Name = "Grocery",
        Description = "Grocery Items",
        }
      };

      modelBuilder.Entity<Category> ().HasData (categories);
    }
  }
}