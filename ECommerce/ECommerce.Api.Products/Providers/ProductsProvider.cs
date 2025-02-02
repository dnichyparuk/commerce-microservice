﻿using AutoMapper;
using ECommerce.Api.Products.DB;
using ECommerce.Api.Products.Interfaces;
using ECommerce.Api.Products.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Products.Providers
{
    public class ProductsProvider : IProductsProvider
    {
        private readonly ProductsDbContext dbContext;
        private readonly ILogger<ProductsProvider> logger;
        private readonly IMapper mapper;

        public async Task<(bool IsSuccess, IEnumerable<Models.Product> Products, string ErrorMessage)> GetProductsAsync()
        {
            try
            {
                var products = await dbContext.Products.ToListAsync();
                if (products != null && products.Any())
                {
                    var result = mapper.Map<IEnumerable<DB.Product>, IEnumerable<Models.Product>>(products);
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

        public async Task<(bool IsSuccess, Models.Product Product, string ErrorMessage)> GetProductAsync(int id)
        {
            try
            {
                var product = await dbContext.Products.FirstOrDefaultAsync(x=>x.Id.Equals(id));
                if (product != null)
                {
                    var result = mapper.Map<DB.Product, Models.Product>(product);
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

        public ProductsProvider(ProductsDbContext dbContext, ILogger<ProductsProvider> logger, IMapper mapper) {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;

            SeedData();
        }

        private void SeedData()
        {
            if (!dbContext.Products.Any()) {
                dbContext.Products.Add(new DB.Product() {
                    Id = 1,
                    Name = "Keyboard",
                    Price = 20,
                    Inventory = 100
                });
                dbContext.Products.Add(new DB.Product()
                {
                    Id = 2,
                    Name = "Mouse",
                    Price = 5,
                    Inventory = 100
                });
                dbContext.Products.Add(new DB.Product()
                {
                    Id = 3,
                    Name = "Monitor",
                    Price = 20,
                    Inventory = 150
                });
                dbContext.Products.Add(new DB.Product()
                {
                    Id = 4,
                    Name = "CPU",
                    Price = 200,
                    Inventory = 100
                });
                dbContext.SaveChanges();
            }
        }

        
    }
}
