using System;
using System.Collections.Generic;

namespace RestaurantMenu.Models.Models;

public partial class Image
{
    public Guid ImageId { get; set; }

    public string Path { get; set; } = null!;

    public virtual ICollection<Menu> Menus { get; set; } = new List<Menu>();

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
