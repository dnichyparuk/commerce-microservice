using AutoMapper;
using ECommerce.Api.Orders.Interfaces;
using ECommerce.Api.Orders.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Orders.Providers
{
    public class OrdersProvider : IOrdersProvider
    {
        private readonly DB.OrdersDbContext dbContext;
        private readonly ILogger<OrdersProvider> logger;
        private readonly IMapper mapper;

        public async Task<(bool IsSuccess, Models.Order Order, string ErrorMessage)> GetOrderAsync(int id)
        {
            try
            {
                var order = await dbContext
                    .Orders
                    .Where(x => x.Id.Equals(id))
                    .FirstOrDefaultAsync();
                if (order != null)
                {
                    var result = mapper.Map<DB.Order, Models.Order>(order);
                    return (true, result, null);
                }
                return (false, null, "Not found");
            }
            catch (Exception e)
            {
                logger.LogError(e.ToString());
                return (false, null, e.Message);
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<Order> Orders, string ErrorMessage)> GetOrdersAsync()
        {
            try
            {
                var orders = await dbContext.Orders.ToListAsync();
                if (orders != null && orders.Any())
                {
                    var result = mapper.Map< IEnumerable<DB.Order>, IEnumerable< Models.Order>>(orders);
                    return (true, result, null);
                }
                return (false, null, "Not found");
            }
            catch (Exception e)
            {
                logger.LogError(e.ToString());
                return (false, null, e.Message);
            }
        }

        public OrdersProvider(
             DB.OrdersDbContext dbContext,
            ILogger<OrdersProvider> logger,
            IMapper mapper) {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;

            SeedData();
        }

        private void SeedData()
        {
            if (!dbContext.Orders.Any())
            {
                dbContext.Orders.Add(new DB.Order()
                {
                    Id = 1,
                    OrderDate = DateTime.Now,
                    CustomerId = 1,
                    Total = 20,
                    OrderItems = (new DB.OrderItem[] { 
                        new DB.OrderItem {
                            Id = 1,
                            ProductId = 1,
                            Count = 1,
                            Price = 20
                        } }).ToList()
                });
                dbContext.Orders.Add(new DB.Order()
                {
                    Id = 2,
                    OrderDate = DateTime.Now,
                    CustomerId = 2,
                });
                dbContext.Orders.Add(new DB.Order()
                {
                    Id = 3,
                    OrderDate = DateTime.Now,
                    CustomerId = 3,
                });
                dbContext.Orders.Add(new DB.Order()
                {
                    Id = 4,
                    OrderDate = DateTime.Now,
                    CustomerId = 1,
                });
                dbContext.SaveChanges();
            }
        }
    }
}
