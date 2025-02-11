namespace MilkStore.Models
{
    public class OrderDetails
    {
        public int OrderId { get; set; } // Primary Key with Identity (101,1)

        public DateTime OrderDate { get; set; } = DateTime.UtcNow; // Default value

        public DateTime DeliveryDate{ get; set; }

        public string OrderStatus { get; set; } = "Pending";

        public string UserEmailId { get; set; } = null!;

        public decimal TotalAmount { get; set; }

        public string ProductName { get; set; } = null!;

        public string? ProductImage { get; set; }

        
    }
}
