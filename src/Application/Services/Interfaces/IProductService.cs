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
        /// Obtiene un producto por su SKU.
        /// </summary>
        /// <param name="ID">ID del producto.</param>
        /// <returns>El producto correspondiente al ID proporcionado.</returns>
        Task<Product?> GetProductByID(string ID);

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

        /// <summary>
        /// Actualiza un producto existente.
        /// </summary>
        /// <param name="SKU">SKU del producto a actualizar.</param>
        /// <param name="product">Producto con los nuevos datos.</param>
        /// <returns>True si se actualizó el producto, false en caso contrario.</returns>
        Task<object?> UpdateProduct(string SKU, UpdateProductDto product);

        /// <summary>
        /// Elimina un producto por su SKU.
        /// </summary>
        /// <param name="SKU">SKU del producto a eliminar.</param>
        /// <returns>True si se eliminó el producto, false en caso contrario.</returns>
        Task<bool> DeleteProduct(string SKU);
    }
}