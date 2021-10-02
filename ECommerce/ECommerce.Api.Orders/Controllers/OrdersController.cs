using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.Api.Orders.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace ECommerce.Api.Orders.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : Controller
    {
        private readonly IOrdersProvider provider;
        private readonly ILogger<OrdersController> logger;
        public OrdersController(IOrdersProvider provider, ILogger<OrdersController> logger) {
            this.provider = provider;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrdersAsync()
        {
            var results = await provider.GetOrdersAsync();
            if (results.IsSuccess)
            {
                return Ok(results.Orders);
            }
            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderAsync(int id)
        {
            var result = await provider.GetOrderAsync(id);
            if (result.IsSuccess)
            {
                return Ok(result.Order);
            }
            return NotFound();
        }
    }
}
