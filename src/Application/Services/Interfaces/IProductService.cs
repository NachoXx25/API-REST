using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrudProducts.Properties.Domain.Models;
using CrudProducts.src.Application.DTOs;

namespace CrudProducts.src.Application.Services.Interfaces
{
    public interface IProductService
    {
        /// <summary>
        /// Obtiene un producto por su ID.
        /// </summary>
        /// <param name="id">ID del producto.</param>
        /// <returns>El producto correspondiente al ID proporcionado.</returns>
        Task<Product?> GetProductById(string id);

        /// <summary>
        /// Obtiene todos los productos.
        /// </summary>
        /// <param name="page">Número de página.</param>
        /// <param name="pageSize">Tamaño de página.</param>
        /// <returns>Lista de productos.</returns>
        Task<List<Product>> GetAllProducts(string page, string pageSize);

        /// <summary>
        /// Crea un nuevo producto.
        /// </summary>
        /// <param name="product">Producto a crear.</param>
        /// <returns>True si se creó el producto, false en caso contrario.</returns>
        Task<bool> CreateProduct(CreateProductDto product);
    }
}