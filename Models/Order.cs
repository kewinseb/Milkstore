using System;
using System.Collections.Generic;

namespace MilkStore.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public string UserEmailId { get; set; } = null!;

    public decimal TotalAmount { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual ICollection<ShipmentDetail> ShipmentDetails { get; set; } = new List<ShipmentDetail>();

    public virtual User UserEmail { get; set; } = null!;
}
