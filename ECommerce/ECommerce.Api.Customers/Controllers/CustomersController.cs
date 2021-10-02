using ECommerce.Api.Customers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Customers.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomersController : Controller
    {
        private readonly ICustomersProvider provider;
        private readonly ILogger<CustomersController> logger;

        public CustomersController(ICustomersProvider provider, ILogger<CustomersController> logger)
        {
            this.provider = provider;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomersAsync()
        {
            var results = await provider.GetCustomersAsync();
            if (results.IsSuccess)
            {
                return Ok(results.Customers);
            }
            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductAsync(int id)
        {
            var result = await provider.GetCustomerAsync(id);
            if (result.IsSuccess)
            {
                return Ok(result.Customer);
            }
            return NotFound();
        }
    }
}
