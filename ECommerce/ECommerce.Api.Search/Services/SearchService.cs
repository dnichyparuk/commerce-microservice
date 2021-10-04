using ECommerce.Api.Search.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Search.Services
{
    public class SearchService : ISearchService
    {
        private readonly IOrderService orderService;
        private readonly IProductsService productService;
        private readonly ICustomerService customerService;

        public SearchService(
            IOrderService orderService, 
            IProductsService productService,
            ICustomerService customerService) {
            this.orderService = orderService;
            this.productService = productService;
            this.customerService = customerService;
        }
        public async Task<(bool isSuccess, dynamic SearchResults)> SearchAsync(int customerId)
        {
            var ordersResult = await this.orderService.GetOrdersAsync(customerId);
            if (ordersResult.IsSuccess) {
                var orders = ordersResult.Orders;

                var productResults = await this.productService.GetProductsAsync();
                var customerResult = await this.customerService.GetCustomerAsync(customerId);
                foreach (var o in orders) {
                    foreach (var oitem in o.OrderItems) {
                        oitem.ProductName = productResults.IsSuccess ?
                            productResults
                            .Products
                            .Where(x => x.Id == oitem.ProductId)
                            .FirstOrDefault()?.Name : "Information is unavaible";
                    }
                }

                if (productResults.IsSuccess) {
                    var result = new
                    {
                        CustomerName = customerResult.IsSuccess ?
                            customerResult.Customer?.Name :
                            "Information is unavailable",
                        Orders = orders
                    };
                    return (true, result);
                }
            }
            return (false, null);
        }
    }
}
