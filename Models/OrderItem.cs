using System;
using System.Collections.Generic;

namespace MilkStore.Models;

public partial class OrderItem
{
    public int OrderDetailsId { get; set; }

    public int OrdersOrderId { get; set; }

    public int ProductProductId { get; set; }

    public int Quantity { get; set; }

    public decimal PriceAtOrder { get; set; }

    public int TrackingNumber { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Order OrdersOrder { get; set; } = null!;

    public virtual Product ProductProduct { get; set; } = null!;
}
