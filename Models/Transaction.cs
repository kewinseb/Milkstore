using System;
using System.Collections.Generic;

namespace MilkStore.Models;

public partial class Transaction
{
    public int TransactionId { get; set; }

    public string UserEmailId { get; set; } = null!;

    public DateTime TransactionDate { get; set; }

    public decimal TotalAmountPaid { get; set; }

    public string PaymentMode { get; set; } = null!;

    public string PaymentStatus { get; set; } = null!;

    public virtual User UserEmail { get; set; } = null!;
}
