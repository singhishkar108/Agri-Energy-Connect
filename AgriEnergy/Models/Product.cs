using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AgriEnergy.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string? ProductName { get; set; }

    public string? ProductDescription { get; set; }

    public double? Price { get; set; }

    public int? Quantity { get; set; }

    [Required(ErrorMessage = "Availability is required.")]
    [RegularExpression("YES|NO", ErrorMessage = "Availability must be either 'YES' or 'NO'.")]
    public string? Availability { get; set; }

    public DateOnly? ProductionDate { get; set; }

    public int? FarmerId { get; set; }

    public int? CategoryId { get; set; }

    public string? ImageUrl { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual Category? Category { get; set; }

    public virtual User? Farmer { get; set; }
}
