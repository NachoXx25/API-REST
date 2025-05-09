using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CrudProducts.src.Application.DTOs
{
    public class UpdateProductDto
    {
        [StringLength(50, ErrorMessage = "El nombre no puede tener más de 50 caracteres")]
        public string? Name { get; set; } 
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "El SKU solo puede contener letras y números")]
        [StringLength(30, ErrorMessage = "El SKU no puede tener más de 30 caracteres")]
        public string? SKU { get; set; } 
        [Range(1, int.MaxValue, ErrorMessage = "El valor del precio debe ser mayor que 0")]
        [RegularExpression(@"^\d+$", ErrorMessage = "El valor del precio debe ser un número entero positivo")]
        public string? Price { get; set; } 
        [Range(0, int.MaxValue, ErrorMessage = "El valor del stock debe ser igual o mayor que 0")]
        [RegularExpression(@"^\d+$", ErrorMessage = "El valor del stock debe ser un número entero positivo")]
        public string? Stock { get; set; } 
    }
}