using System;
using System.Collections.Generic;

namespace MilkStore.Models;

public partial class ShipmentDetail
{
    public int ShipmentId { get; set; }

    public string ShippingAddress { get; set; } = null!;

    public string DeliveryStatus { get; set; } = null!;

    public DateTime DeliveryDate { get; set; }

    public int OrdersOrderId { get; set; }

    public virtual Order OrdersOrder { get; set; } = null!;
}
