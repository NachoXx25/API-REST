using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CrudProducts.src.Application
{
    public class PaginationDTO
    {   
        [Required (ErrorMessage = "El campo de la página es requerido")]
        [Range(1, int.MaxValue, ErrorMessage = "El valor de la página debe ser mayor que 0")]
        [RegularExpression(@"^\d+$", ErrorMessage = "El valor de la página debe ser un número entero positivo")]
        public required string Page { get; set; }
        [Required (ErrorMessage = "El campo de tamaño de página es requerido")]
        [Range(1, int.MaxValue, ErrorMessage = "El valor de tamaño de página debe ser mayor que 0")]
        [RegularExpression(@"^\d+$", ErrorMessage = "El valor de tamaño de página debe ser un número entero positivo")]
        public required string PageSize { get; set; }
    }
}