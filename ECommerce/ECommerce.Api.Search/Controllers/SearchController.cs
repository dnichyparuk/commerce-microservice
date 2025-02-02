﻿using ECommerce.Api.Search.Interfaces;
using ECommerce.Api.Search.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Search.Controllers
{
    [ApiController]
    [Route("api/search")]
    public class SearchController: ControllerBase
    {
        private readonly ISearchService searchService;

        public SearchController(ISearchService searchService) {
            this.searchService = searchService;
        }
        [HttpPost]
        public async Task<IActionResult> SearchAsync(SearchTerm term) {
            var result = await searchService.SearchAsync(term.CustomerId);
            if (result.isSuccess) {
                return Ok(result.SearchResults);
            }
            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> SearchByCustomerAsync(int id) {
            var result = await searchService.SearchAsync(id);
            if (result.isSuccess)
            {
                return Ok(result.SearchResults);
            }
            return NotFound();
        }
    }
}
