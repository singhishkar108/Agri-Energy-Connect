using System;
using System.Collections.Generic;

namespace AgriEnergy.Models;

public partial class User
{
    public int UserId { get; set; }

    public string? FirebaseUid { get; set; }

    public string? Role { get; set; }

    public string? Name { get; set; }

    public DateOnly? Dob { get; set; }

    public string? Bio { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
