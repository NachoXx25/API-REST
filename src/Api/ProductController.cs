using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrudProducts.src.Application;
using CrudProducts.src.Application.DTOs;
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

        /// <summary>
        /// Obtiene un producto por su ID.
        /// </summary>
        /// <param name="id">ID del producto.</param>
        /// <returns>El producto correspondiente al ID proporcionado.</returns>
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetProductById(string id)
        {
            try{
                var product = await _productService.GetProductById(id);
                if (product == null)
                {
                    return NotFound(new { message = "Producto no encontrado" });
                }
                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error interno del servidor", error = ex.Message });
            }
        }

        /// <summary>
        /// Obtiene todos los productos.
        /// </summary>
        /// <param name="paginationDTO">DTO de paginación.</param>
        /// <returns>Lista de productos.</returns>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllProducts([FromQuery] PaginationDTO paginationDTO)
        {
            if(!ModelState.IsValid)return BadRequest(new { ModelState });
            try{
                var products = await _productService.GetAllProducts(paginationDTO.Page, paginationDTO.PageSize);
                if (products == null || products.Count == 0)
                {
                    return NotFound(new { message = "No se encontraron productos" });
                }
                return Ok(products);
            }catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error interno del servidor", error = ex.Message });
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto productDto)
        {
            if(!ModelState.IsValid) return BadRequest(new { ModelState });
            try
            {
                var product = await _productService.CreateProduct(productDto);
                if (!product)
                {
                    return BadRequest(new { message = "El producto ya existe" });
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error interno del servidor", error = ex.Message });
            }
        }
    }
}