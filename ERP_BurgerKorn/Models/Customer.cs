using System.ComponentModel.DataAnnotations;

namespace ERP_BurgerKorn.Models
{
    public class Customer
    {
        [Key] // primary key
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Customer Name is required")]
        [MaxLength(100)]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Customer Email is required")]
        [MaxLength(255)]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Customer Phone no. is required")]
        [MaxLength(50)]
        public required string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Customer Address is required")]
        [MaxLength(255)]
        public  required string Address { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now; // Default ตอนบันทึก



        // realation : customer - > Order (one customer have many orders)
        public List<Order>? Orders { get; set; }
    }
}
