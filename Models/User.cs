using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace MilkStore.Models;

public partial class User
{
    public int UserId { get; set; }

    [Required(ErrorMessage = "Name is required.")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "Email is required.")]
    [RegularExpression(@"^[a-zA-Z0-9._%+-]+@(gmail\.com|microsoft\.com|yahoo\.com|outlook\.com|hotmail\.com|protonmail\.com|aol\.com|zohomail\.com|.+\.edu|.+\.gov|.+\.org|.+\.co\.uk|.+\.in)$",
    ErrorMessage = "Email must be a valid email address.")]
    public string EmailId { get; set; } = null!;

    [Required(ErrorMessage = "Password is required.")]
    [StringLength(16, ErrorMessage = "Password must be between 8 and 16 characters long.", MinimumLength = 8)]
    [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,16}$",
    ErrorMessage = "Password must contain at least one letter, one number, and one special character.")]
    public string Password { get; set; } = null!;

    public string? Address { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    [RegularExpression(@"^\d{6}$", ErrorMessage = "Pin code must be a 6-digit number.")]
    public int? PinCode { get; set; }

    [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be a 10-digit number.")]
    public string? Phone { get; set; }

    public string Role { get; set; } = "user"!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<CardDetail> CardDetails { get; set; } = new List<CardDetail>();

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual ICollection<Orders> Orders { get; set; } = new List<Orders>();

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
