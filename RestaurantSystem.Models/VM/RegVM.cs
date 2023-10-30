using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem.Models.VM
{
    public class RegVM : LoginVM
    {

        [Required(ErrorMessage = "Заполните обязательное поле")]
        [EmailAddress(ErrorMessage = "Неверный формат электронной почты")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Заполните обязательное поле")]
        [Phone(ErrorMessage = "Неккоректный формат телефона")]
        public string PhoneNumber { get; set; }
    }
}
