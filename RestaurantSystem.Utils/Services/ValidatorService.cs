using RestaurantMenu.Models.Models;
using RestaurantMenu.Utils.IServices;
using RestaurantSystem.Models.VM;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantMenu.Utils.Services
{
    public class ValidatorService : IValidatorService
    {       
        public bool ValidateObject(object obj) 
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
