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
    public class CustomerService : ICustomerService
    {
        private readonly ILogger<CustomerService> logger;
        private readonly IHttpClientFactory httpClientFactory;

        public CustomerService(IHttpClientFactory httpClientFactory, ILogger<CustomerService> logger) {
            this.logger = logger;
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<(bool IsSuccess, Customer Customer, string ErrorMessage)> GetCustomerAsync(int id)
        {
            try
            {
                var client = httpClientFactory.CreateClient("CustomersService");
                var response = await client.GetAsync($"api/customers/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStreamAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var customer = await JsonSerializer
                        .DeserializeAsync<Customer>(content, options);
                    return (true, customer, response.ReasonPhrase);
                }
                return (false, null, "Not found");
            }
            catch (Exception e)
            {
                logger.LogError(e.ToString());
                return (false, null, e.Message);
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<Customer> Customers, string ErrorMessage)> GetCustomersAsync()
        {
            try
            {
                var client = httpClientFactory.CreateClient("CustomersService");
                var response = await client.GetAsync("api/customers");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStreamAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var customers = await JsonSerializer
                        .DeserializeAsync<IEnumerable<Customer>>(content, options);
                    return (true, customers, response.ReasonPhrase);
                }
                return (false, null, "Not found");
            }
            catch (Exception e)
            {
                logger.LogError(e.ToString());
                return (false, null, e.Message);
            }
        }
    }
}
