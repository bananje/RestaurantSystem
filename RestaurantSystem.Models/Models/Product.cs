using System;
using System.Collections.Generic;

namespace RestaurantMenu.Models.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public float Price { get; set; }

    public float Weight { get; set; }

    public string WeightValue { get; set; } = null!;

    public int MenuId { get; set; }

    public int CategoryId { get; set; }

    public Guid? ImageId { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual Image? Image { get; set; }

    public virtual Menu Menu { get; set; } = null!;
}
