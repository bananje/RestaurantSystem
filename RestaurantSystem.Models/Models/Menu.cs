using System;
using System.Collections.Generic;

namespace RestaurantMenu.Models.Models;

public partial class Menu
{
    public int MenuId { get; set; }

    public int CategoryId { get; set; }

    public Guid ImageId { get; set; }

    public string Name { get; set; } 

    public virtual Category Category { get; set; } = null!;

    public virtual Image Image { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
