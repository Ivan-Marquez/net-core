using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ProductService.Entities;

namespace ProductService.Repository {
    public interface IProductRepository {
        Task<IEnumerable<Product>> GetAllProductsAsync ();
        Task<IQueryable<Product>> GetProductByAsync (Expression<Func<Product, bool>> condition);
        Task<Product> AddProductAsync (Product product);
        Task<Product> UpdateProductAsync (Product product);
        Task DeleteProductAsync (int productId);
        Task SaveAsync ();
    }
}