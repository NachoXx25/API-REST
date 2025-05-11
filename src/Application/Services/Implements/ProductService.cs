using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrudProducts.Properties.Domain.Models;
using CrudProducts.Properties.Infrastructure.Data;
using CrudProducts.src.Application.DTOs;
using CrudProducts.src.Application.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CrudProducts.src.Application.Services.Implements
{
    public class ProductService : IProductService
    {
        private readonly DataContext _context;
        public ProductService(DataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Crea un nuevo producto.
        /// </summary>
        /// <param name="product">Producto a crear.</param>
        /// <returns>True si se creó el producto, false en caso contrario.</returns>
        public async Task<bool> CreateProduct(CreateProductDto product)
        {
            var veriFyProduct = await _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.SKU == product.SKU);
            if (veriFyProduct != null) return false;

            var newProduct = new Product
            {
                Name = product.Name,
                SKU = product.SKU,
                Price = int.Parse(product.Price),
                Stock = int.Parse(product.Stock)
            };
           await _context.Products.AddAsync(newProduct);
           await _context.SaveChangesAsync();
           return true;
        }

        /// <summary>
        /// Elimina un producto por su SKU.
        /// </summary>
        /// <param name="ID">ID del producto a eliminar.</param>
        /// <returns>True si se eliminó el producto, false en caso contrario.</returns>
        public async Task<object> DeleteProduct(string ID)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id.ToString() == ID);
            if (product == null) return false;
            if(product.IsActive == false) return -1;
            product.IsActive = false;
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Obtiene todos los productos.
        /// </summary>
        /// <param name="page">Número de página.</param>
        /// <param name="pageSize">Tamaño de página.</param>
        /// <returns>Lista de productos.</returns>
        public async Task<List<Product>> GetAllProducts(string page, string pageSize)
        {

            return await _context.Products.Skip((int.Parse(page) - 1) * int.Parse(pageSize)).Take(int.Parse(pageSize)).ToListAsync();
        }

        /// <summary>
        /// Obtiene un producto por su ID.
        /// </summary>
        /// <param name="ID">ID del producto.</param>
        /// <returns>El producto correspondiente al ID proporcionado.</returns>
        public async Task<Product?> GetProductByID(string ID)
        {
            return await _context.Products.AsNoTracking().FirstOrDefaultAsync( p => p.Id.ToString() == ID);
        }

        /// <summary>
        /// Actualiza un producto existente.
        /// </summary>
        /// <param name="ID">ID del producto a actualizar.</param>
        /// <param name="product">Producto con los nuevos datos.</param>
        /// <returns>True si se actualizó el producto, false en caso contrario.</returns>
        public async Task<object?> UpdateProduct(string ID, UpdateProductDto product)
        {
            var verifyProduct = await _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id.ToString() == ID);
            if (verifyProduct == null) return false;
            if (!string.IsNullOrWhiteSpace(product.SKU) && product.SKU != verifyProduct.SKU)
            {
                
                var existingSku = await _context.Products.AsNoTracking().AnyAsync(p => p.SKU == product.SKU);
                    
                if (existingSku) return -1; 
            }
            bool unchanged = 
                (product.Name ?? "") == verifyProduct.Name && 
                (product.SKU ?? "") == verifyProduct.SKU &&
                (product.Price ?? "") == verifyProduct.Price.ToString() && 
                (product.Stock ?? "") == verifyProduct.Stock.ToString();
            
            if (unchanged) return null;
            
            if (!string.IsNullOrWhiteSpace(product.Name))
                verifyProduct.Name = product.Name;
            
            if (!string.IsNullOrWhiteSpace(product.SKU))
                verifyProduct.SKU = product.SKU;
            
            if (!string.IsNullOrWhiteSpace(product.Price))
                verifyProduct.Price = int.Parse(product.Price);
            
            if (!string.IsNullOrWhiteSpace(product.Stock))
                verifyProduct.Stock = int.Parse(product.Stock);

            _context.Products.Update(verifyProduct);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}