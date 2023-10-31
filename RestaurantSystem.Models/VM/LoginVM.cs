using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RestaurantSystem.Models.VM
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Не заполнено имя пользователя")]
        [Display(Name = "Логин")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Не заполнено поле пароля")]
        [Display(Name = "Пароль")]

        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@#$%^&+=]).{6,}$", ErrorMessage = "Введён неккоректный пароль")]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberLogin { get; set; }
        public string Button { get; set; }
    }
}
