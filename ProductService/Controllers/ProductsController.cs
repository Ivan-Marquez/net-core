using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductService.Entities;
using ProductService.Repository;

namespace ProductService.Controllers {
    [ApiController]
    [Route ("api/[controller]")]
    public class ProductsController : ControllerBase {
        public ProductsController (ILogger<ProductsController> logger, IProductRepository repository) {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get () {
            var products = await _repository.GetAllProductsAsync ();
            return new OkObjectResult (products);
        }

        [HttpGet ("{id}", Name = "Get")]
        public async Task<IActionResult> Get (int id) {
            var product = await _repository.GetProductByAsync (p => p.Id == id);
            return new OkObjectResult (product);
        }

        [HttpPost]
        public async Task<IActionResult> Post ([FromBody] Product product) {
            using (var scope = new TransactionScope ()) {
                await _repository.AddProductAsync (product);
                scope.Complete ();
                return CreatedAtAction (nameof (Get), new { id = product.Id }, product);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put ([FromBody] Product product) {
            if (product != null) {
                using (var scope = new TransactionScope ()) {
                    await _repository.UpdateProductAsync (product);
                    scope.Complete ();
                    return new OkResult ();
                }
            }
            return new NoContentResult ();
        }

        [HttpDelete ("{id}")]
        public async Task<IActionResult> Delete (int id) {
            await _repository.DeleteProductAsync (id);
            return new OkResult ();
        }

        private readonly ILogger<ProductsController> _logger;
        private readonly IProductRepository _repository;
    }
}