using System.ComponentModel.DataAnnotations;    
namespace ERP_BurgerKorn.Models
{
    public class Supplier
    {
        [Key] // primary key
        public int SupplierId { get; set; }

        [Required(ErrorMessage = "Supplier Name is required")]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(255)]
        public string ContactInfo { get; set; }

        // realation : supplier - > Product (one supplier have many products)
        public List<Product> Products { get; set; } 

    }
}
