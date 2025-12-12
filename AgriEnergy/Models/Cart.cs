using System;
using System.Collections.Generic;

namespace AgriEnergy.Models;

public partial class Cart
{
    public int CartId { get; set; }

    public int? ProductId { get; set; }

    public virtual Product? Product { get; set; }
}
