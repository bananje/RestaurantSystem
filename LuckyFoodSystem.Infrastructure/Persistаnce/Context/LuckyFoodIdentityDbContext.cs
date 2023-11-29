using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using LuckyFoodSystem.Application.Common.Models;

namespace LuckyFoodSystem.Identity.Data;

public class LuckyFoodIdentityDbContext : IdentityDbContext<LuckyFoodUser>
{
    public LuckyFoodIdentityDbContext(DbContextOptions<LuckyFoodIdentityDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);;
    }
}
