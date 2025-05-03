using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP_BurgerKorn.Models
{
    public class Product
    {
        [Key] // primary key
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Product Name is required")]
        [MaxLength(100)]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Stok Keeping Unit is required")]
        [MaxLength(50)]
        public required string SKU { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public int StockQuantity { get; set; }

        // Realation: Product -> Supplioer (maSny product have oneWS supplier)
        public int SupplierId { get; set; }
        public Supplier? Supplier { get; set; }


    }
}
