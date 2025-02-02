﻿using AutoMapper;
using ECommerce.Api.Customers.Models;
using ECommerce.Api.Customers.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Customers.Providers
{
    public class CustomersProvider : ICustomersProvider
    {
        private readonly DB.CustomersDbContext dbContext;
        private readonly ILogger<CustomersProvider> logger;
        private readonly IMapper mapper;


        public async Task<(bool IsSuccess, Models.Customer Customer, string ErrorMessage)> GetCustomerAsync(int id)
        {
            try
            {
                var customer = await dbContext
                    .Customers
                    .FirstOrDefaultAsync(x=>x.Id.Equals(id));
                if (customer != null)
                {
                    var result = mapper.Map<DB.Customer,Models.Customer>(customer);
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

        public async Task<(bool IsSuccess, IEnumerable<Models.Customer> Customers, string ErrorMessage)> GetCustomersAsync()
        {
            try
            {
                var customers = await dbContext.Customers.ToListAsync();
                if (customers != null && customers.Any())
                {
                    var result = mapper.Map<IEnumerable<DB.Customer>, IEnumerable<Models.Customer>>(customers);
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

        public CustomersProvider(
            DB.CustomersDbContext dbContext,
            ILogger<CustomersProvider> logger,
            IMapper mapper) {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;

            SeedData();
        }

        private void SeedData()
        {
            if (!dbContext.Customers.Any())
            {
                dbContext.Customers.Add(new DB.Customer()
                {
                    Id = 1,
                    Name = "Peter",
                });
                dbContext.Customers.Add(new DB.Customer()
                {
                    Id = 2,
                    Name = "Alex",
                });
                dbContext.Customers.Add(new DB.Customer()
                {
                    Id = 3,
                    Name = "John",
                });
                dbContext.Customers.Add(new DB.Customer()
                {
                    Id = 4,
                    Name = "Sergey",
                });
                dbContext.SaveChanges();
            }
        }
    }
}
