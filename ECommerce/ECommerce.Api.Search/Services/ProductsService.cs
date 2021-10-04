using ECommerce.Api.Search.Interfaces;
using ECommerce.Api.Search.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ECommerce.Api.Search.Services
{
    public class ProductsService : IProductsService
    {
        private readonly ILogger<ProductsService> logger;
        private readonly IHttpClientFactory httpClientFactory;

        public ProductsService(
            IHttpClientFactory httpClientFactory,
            ILogger<ProductsService> logger) {

            this.logger = logger;
            this.httpClientFactory = httpClientFactory;
        }
        public async Task<(bool IsSuccess, IEnumerable<Product> Products, string ErrorMessage)> GetProductsAsync()
        {
            try {
                var client = httpClientFactory.CreateClient("ProductsService");
                var response = await client.GetAsync("api/products");
                if (response.IsSuccessStatusCode) {
                    var content = await response.Content.ReadAsStreamAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var products = await JsonSerializer
                        .DeserializeAsync<IEnumerable<Product>>(content, options);
                    return (true, products, response.ReasonPhrase);
                }
                return (false, null, "Not found");
            } catch (Exception e) { 
                logger.LogError(e.ToString());
                return (false, null, e.Message);
            }
        }
    }
}
