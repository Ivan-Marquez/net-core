using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProductService.Entities;
using ProductService.Storage;

namespace ProductService.Repository {
  public class ProductRepository : IProductRepository {

    public ProductRepository (ProductContext db) {
      _db = db;
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync () {
      var products = await _db.Products.ToListAsync ();

      return products;
    }

    public Task<IQueryable<Product>> GetProductByAsync (Expression<Func<Product, bool>> condition) {
      IQueryable<Product> product = _db.Products.Where (condition);

      return Task.FromResult (product);
    }

    public async Task<Product> AddProductAsync (Product product) {
      await _db.Products.AddAsync (product);
      await SaveAsync ();

      return product;
    }

    public async Task<Product> UpdateProductAsync (Product product) {
      var p = await _db.Products.FindAsync (product.Id);
      p.Name = product.Name;
      p.Description = product.Description;
      p.Price = product.Price;
      p.CategoryId = product.CategoryId;

      await SaveAsync ();

      return product;
    }

    public async Task DeleteProductAsync (int productId) {
      var product = await _db.Products.FindAsync (productId);
      if (product == null) {
        return;
      }

      _db.Products.Remove (product);
      await SaveAsync ();
    }

    public Task SaveAsync () {
      return _db.SaveChangesAsync ();
    }

    private readonly ProductContext _db;
  }
}