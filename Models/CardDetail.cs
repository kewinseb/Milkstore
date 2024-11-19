using System;
using System.Collections.Generic;

namespace MilkStore.Models;

public partial class CardDetail
{
    public int CardId { get; set; }

    public string UserEmailId { get; set; } = null!;

    public string CardNumber { get; set; } = null!;

    public string Cvv { get; set; } = null!;

    public virtual User UserEmail { get; set; } = null!;
}
