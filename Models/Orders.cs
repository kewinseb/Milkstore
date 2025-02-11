using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MilkStore.Models
{
    public class Orders
    {
       
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int OrderId { get; set; } // Primary Key with Identity (101,1)

            [Required]
            [ForeignKey("User")]
            public string UserEmailId { get; set; } = null!; // FK to Users table

            [Required]
            public DateTime OrderDate { get; set; } = DateTime.UtcNow; // Default value

            [Required]
            public int OrderQuantity { get; set; } // Total number of products in the order

            [Required]
            [Column(TypeName = "decimal(10,2)")]
            public decimal TotalAmount { get; set; } // Total price of all products in order

            [Required]
            [Column(TypeName = "varchar(50)")]
            public string OrderStatus { get; set; } = "Pending"; // Default status

            public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
            public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

            // Navigation Property
            public virtual User User { get; set; } = null!;
    }       
    
}
