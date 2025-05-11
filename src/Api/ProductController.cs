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
        /// <param name="ID">ID del producto.</param>
        /// <returns>El producto correspondiente al ID proporcionado.</returns>
        [HttpGet("{ID}")]
        [Authorize]
        public async Task<IActionResult> GetProductByID(string ID)
        {
            try{
                var product = await _productService.GetProductByID(ID);
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

        /// <summary>
        /// Crea un nuevo producto.
        /// </summary>
        /// <param name="productDto">DTO del producto a crear.</param>
        /// <returns>Resultado de la creación del producto.</returns>
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
                return Created();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error interno del servidor", error = ex.Message });
            }
        }

        /// <summary>
        /// Actualiza un producto existente.
        /// </summary>
        /// <param name="ID">ID del producto a actualizar.</param>
        /// <param name="productDto">DTO del producto con los nuevos datos.</param>
        /// <returns>Resultado de la actualización del producto.</returns>
        [HttpPatch("{ID}")]
        [Authorize]
        public async Task<IActionResult> UpdateProduct(string ID, [FromForm] UpdateProductDto productDto)
        {
            if(!ModelState.IsValid) return BadRequest(new { ModelState });
            if(string.IsNullOrWhiteSpace(productDto.Name) && string.IsNullOrWhiteSpace(productDto.SKU) && string.IsNullOrWhiteSpace(productDto.Price) && string.IsNullOrWhiteSpace(productDto.Stock)) return BadRequest(new { message = "No se han proporcionado datos para actualizar el producto" });
            try
            {
                var result = await _productService.UpdateProduct(ID, productDto);
                if (result is bool boolResult)
                {
                    return boolResult ? NoContent() : NotFound(new { message = "Producto no encontrado" });
                }
                else if (result is null)
                {
                    return BadRequest(new { message = "No se han realizado cambios en el producto" });
                }
                else {
                    return BadRequest(new { message = "El SKU ya existe" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error interno del servidor", error = ex.Message });
            }
        }

        /// <summary>
        /// Elimina un producto por su SKU.
        /// </summary>
        /// <param name="ID">ID del producto a eliminar.</param>
        /// <returns>Resultado de la eliminación del producto.</returns>
        [HttpDelete("{ID}")]
        [Authorize]
        public async Task<IActionResult> DeleteProduct(string ID)
        {
            try
            {
                var result = await _productService.DeleteProduct(ID);
                if (result) return NoContent();
                else
                {
                    return NotFound(new { message = "Producto no encontrado" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error interno del servidor", error = ex.Message });
            }
        }
    }
}