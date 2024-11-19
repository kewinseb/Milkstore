using System;
using System.Collections.Generic;

namespace MilkStore.Models;

public partial class Cart
{
    public int CartId { get; set; }

    public string UserEmailId { get; set; } = null!;

    public int ProductProductId { get; set; }

    public int Quantity { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Product ProductProduct { get; set; } = null!;

    public virtual User UserEmail { get; set; } = null!;
}
