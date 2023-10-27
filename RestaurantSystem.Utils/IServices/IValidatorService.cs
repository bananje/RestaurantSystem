using RestaurantMenu.Models.Models;
using RestaurantSystem.Models.VM;

namespace RestaurantMenu.Utils.IServices
{
    public interface IValidatorService
    {
        bool ValidateModel(object obj);
    }
}
