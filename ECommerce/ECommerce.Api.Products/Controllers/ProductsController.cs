using ECommerce.Api.Products.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Products.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController: ControllerBase
    {
        private readonly IProductsProvider provider;
        private readonly ILogger<ProductsController> logger;

        public ProductsController(IProductsProvider provider, ILogger<ProductsController> logger) {
            this.provider = provider;
            this.logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> GetProductsAsync()
        {
            var results = await provider.GetProductsAsync();
            if (results.IsSuccess) {
                return Ok(results.Products);
            }
            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductAsync(int id) {
            var result = await provider.GetProductAsync(id);
            if (result.IsSuccess)
            {
                return Ok(result.Product);
            }
            return NotFound();
        }
    }
}
