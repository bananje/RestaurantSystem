using RestaurantMenu.Utils.IServices;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace RestaurantMenu.Utils.Services
{
    public class ValidatorService : IValidatorService
    {       
        public bool ValidateModel(object obj) 
        {
            var validaionContext = new ValidationContext(obj);
            var validationResults = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(obj, validaionContext, validationResults, true);
            if (!isValid)
            {
                foreach (var result in validationResults)
                {
                    Console.WriteLine(result);
                }
                return false;
            }
            return true;
        }
    }
}
