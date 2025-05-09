using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrudProducts.Properties.Domain.Models;
using CrudProducts.Properties.Infrastructure.Data;
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
        /// <param name="id">ID del producto.</param>
        /// <returns>El producto correspondiente al ID proporcionado.</returns>
        public async Task<Product?> GetProductById(string id)
        {
            return await _context.Products.AsNoTracking().FirstOrDefaultAsync( p => p.Id.ToString() == id);
        }
    }
}