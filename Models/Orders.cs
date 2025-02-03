using System.ComponentModel.DataAnnotations;

namespace MilkStore.Models
{
    public class Orders
    {
        [Key]
        public int OrderId { get; set; }  // Primary Key

        [Required]
        public DateTime OrderDate { get; set; } = DateTime.Now;  // Default value as current date

        public DateTime? DeliveryDate { get; set; }  // Nullable in case delivery is not set

        [Required]
        [Range(0.01, 99999.99)]
        public decimal TotalAmount { get; set; }

        [Required]
        [MaxLength(50)]
        public string OrderStatus { get; set; } = "Pending";  // Default status

        [Required]
        [MaxLength(255)]
        public string ShippingAddress { get; set; }

        [Required]
        [MaxLength(255)]
        public string ProductName { get; set; }  // Product name as a string

        [Required]
        [MaxLength(255)]
        public string ProductImage { get; set; }  // Image URL or file path
    }
}
