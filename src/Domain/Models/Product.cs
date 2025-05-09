using System.ComponentModel.DataAnnotations.Schema;


namespace CrudProducts.Properties.Domain.Models
{
    public class Product
    {
        public Guid Id { get; set; } = Guid.NewGuid();    
        [Column(TypeName = "varchar(50)")]
        public string Name { get; set; } = string.Empty;

        [Column(TypeName = "varchar(30)")]
        public string SKU { get; set; } = string.Empty;

        public int Price { get; set; }

        public int Stock { get; set; }

        public bool IsActive { get; set; }
    }
}