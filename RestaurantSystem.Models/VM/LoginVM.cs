using System.ComponentModel.DataAnnotations;

namespace RestaurantSystem.Models.VM
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Не заполнено имя пользователя")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Не заполнено поле пароля")]
        public string Password { get; set; }

        [Required(ErrorMessage = "ReturnUrl is required")]
        public string ReturnUrl { get; set; }
    }

}
