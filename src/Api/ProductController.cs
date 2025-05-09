using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrudProducts.src.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CrudProducts.src.Api
{
    [ApiController]
    [Route("productos")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetProductById(string id)
        {
            var product = await _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound(new { message = "Producto no encontrado" });
            }
            return Ok(product);
        }
    }
}