using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RestaurantSystem.Models.Models;

namespace RestaurantSystem.Models
{
    public class RestaurantSystemIdentityDb : IdentityDbContext<WebAppUser>
    {
        public RestaurantSystemIdentityDb()
        {           
        }

        public RestaurantSystemIdentityDb(DbContextOptions<RestaurantSystemIdentityDb> options) : base(options)
        {           
        }
    }
}
