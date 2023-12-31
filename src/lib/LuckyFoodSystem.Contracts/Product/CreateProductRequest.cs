﻿using Microsoft.AspNetCore.Http;

namespace LuckyFoodSystem.Contracts.Product
{
    public record CreateProductRequest(
            string Title,
            string Description,
            string ShortDescription,
            float Price,
            float WeightValue,
            string WeightUnit,
            string Category,
            HashSet<Guid> MenusIds,
            IFormFileCollection? Files = null);
}
