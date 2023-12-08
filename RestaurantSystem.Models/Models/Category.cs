using System;
using System.Collections.Generic;

namespace RestaurantMenu.Models.Models;

public partial class Category
{
    public int CategoryId { get; set; }

    public string Title { get; set; } = null!;

    public virtual ICollection<Menu> Menus { get; set; } = new List<Menu>();

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
